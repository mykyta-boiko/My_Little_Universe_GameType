using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using ResourceLogic;
using Data;
using System;

namespace IslandLogic
{
    [Serializable]
    public class ResourceTypeIntDictionary
    {
        [SerializeField] public ResourceType Type;
        [SerializeField] public int ResourceAmount;
    }

    public class IslandBuilder : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _island;
        [SerializeField] private SpriteRenderer _buildZone;

        [Space(5)] [Header("Build Settings")]
        [SerializeField] private List<IslandBuilder> _nextIslandsBuilder;
        [SerializeField] private List<ResourceTypeIntDictionary> _resources;

        private Player _player;
        private const float TAKE_RESOURCE_DELAY = 0.2f;
        private List<ResourceTypeIntDictionary> _resourceToRemove = new List<ResourceTypeIntDictionary>();


        private void OnTriggerEnter(Collider other)
        {
            _player = other.GetComponent<Player>();
            StartCoroutine(TakeResourse());
        }

        private void OnTriggerExit(Collider other)
        {
            StopCoroutine(TakeResourse());
        }

        private IEnumerator TakeResourse()
        {
            while (_resources.Count > 0)
            {
                yield return new WaitForSeconds(TAKE_RESOURCE_DELAY);
                foreach (var resource in _resources)
                {
                    if (resource.ResourceAmount > 0)
                    {
                        if(_player.BuildIsland(resource.Type))
                        {
                            resource.ResourceAmount--;
                        }
                    }
                    else
                    {
                        _resourceToRemove.Add(resource);
                        break;
                    }
                }
                if (_resourceToRemove.Count > 0)
                {
                    _resources.Remove(_resourceToRemove[0]);
                }
            }
            DeactivateBuilder();
            DataController.SaveLastBuiltIslandName(gameObject.name);
        }

        public void DeactivateBuilder()
        {
            foreach (IslandBuilder builder in _nextIslandsBuilder)
            {
                builder.gameObject.SetActive(true);
            }
            _island.SetActive(true);
            _collider.enabled = false;
            _buildZone.enabled = false;
            StopCoroutine(TakeResourse());
        }
    }
}
