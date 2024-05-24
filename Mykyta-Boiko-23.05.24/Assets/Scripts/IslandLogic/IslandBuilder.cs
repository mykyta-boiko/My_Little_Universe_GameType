using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using ResourceLogic;

namespace IslandLogic
{
    public class IslandBuilder : MonoBehaviour
    {
        [SerializeField] private Dictionary<ResourceType, int> _resources;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _island;
        private Player _player;
        private const float TAKE_RESOURCE_DELAY = 0.2f;

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
                    if (resource.Value > 0)
                    {
                        _player.BuildIsland(resource.Key);
                    }
                    else
                    {
                        _resources.Remove(resource.Key);
                    }
                }
                Debug.Log("TAKE RESOURCE IS WORKING");
            }
            _island.SetActive(true);
            gameObject.GetComponent<IslandBuilder>().enabled = false;
        }
    }
}
