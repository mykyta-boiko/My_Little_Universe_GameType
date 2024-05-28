using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Resource;
using Data;
using System;
using ObjectPool;
using PlayerLogic;

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
        [SerializeField] private GameObject _resourcePanelContainer;
        [SerializeField] private List<ResourceRequirementsPanel> _resourcePanels;
        [SerializeField] private ObjectPooler.ObjectInfo.ObjectTypes _spendResource;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _island;
        [SerializeField] private SpriteRenderer _buildZone;

        [Space(5)]
        [Header("Build Settings")]
        [SerializeField] private List<IslandBuilder> _nextIslandsBuilder;
        [SerializeField] private List<ResourceTypeIntDictionary> _resources;
        [SerializeField] private List<ObjectPooler.ObjectInfo.ObjectTypes> _spawnResources;

        private List<ResourceTypeIntDictionary> _resourceToRemove = new List<ResourceTypeIntDictionary>();
        private List<int> _currentSpendedResource = new List<int>();
        private Player _player;

        private const float TAKE_RESOURCE_DELAY = 0.1f;
        private Coroutine _takeResource;
        private bool _playerInBuildZone = false;

        // Scale island settings
        private Vector3 _islandStartScale;
        private const float DELAY_BEFORE_SCALE_ISLAND = 0.01f;
        private const float TIME_IN_UPSCALE = 0.04f;
        private const float ISLAND_UPSCALE_SIZE = 1.2f;
        private const float ISLAND_UPSCALE_TIME = 0.2f;
        private const float ISLAND_DOWN_SCALE_TIME = 0.15f;

        private void Start()
        {
            _islandStartScale = _island.transform.localScale;

            if (_collider.enabled)
            {
                _island.SetActive(false);
                for (int i = 0; i < _resources.Count; i++)
                {
                    _currentSpendedResource.Add(0);
                    _resourcePanels[i].SetStartView(_currentSpendedResource[i], 
                        _resources[i].ResourceAmount, _resources[i].Type);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_player == null)
            {
                _player = other.GetComponent<Player>();
            }
            _playerInBuildZone = true;
            _takeResource = StartCoroutine(TakeResource());
        }

        private void OnTriggerExit(Collider other)
        {
            _playerInBuildZone = false;
        }

        private void RemoveUnusedItems()
        {
            _resources.Remove(_resourceToRemove[0]);
            for (int i = _resourcePanels.Count - 1; i >= 0; i--)
            {
                Debug.Log(_resourcePanels[i].TryDeactivate(_resourceToRemove[0].Type));
                if (_resourcePanels[i].TryDeactivate(_resourceToRemove[0].Type))
                {
                    _resourcePanels.RemoveAt(i);
                    _currentSpendedResource.RemoveAt(i);
                    _spawnResources.RemoveAt(i);
                }
            }
            _resourceToRemove.RemoveAt(0);
        }

        private IEnumerator TakeResource()
        {
            while (_resources.Count > 0 && _playerInBuildZone)
            {
                yield return new WaitForSeconds(TAKE_RESOURCE_DELAY);
                for (int i = 0; i < _resources.Count; i++)
                {
                    if (_resources[i].ResourceAmount > 0)
                    {
                        if (_player.BuildIsland(_resources[i].Type))
                        {
                            SpawnResource(_spawnResources[i]);
                            _currentSpendedResource[i]++;
                            _resourcePanels[i].UpdateView(_currentSpendedResource[i]);
                            _resources[i].ResourceAmount--;
                            ObjectPooler.Instance.GetObject(_spendResource, transform.position, transform.rotation);
                        }
                    }
                    else
                    {
                        _resourceToRemove.Add(_resources[i]);
                        break;
                    }
                }
                if (_resourceToRemove.Count > 0)
                {
                    RemoveUnusedItems();
                }
            }
            if (_playerInBuildZone)
            {
                DataController.SaveLastBuiltIslandName(gameObject.name);
                StartCoroutine(BuildIsland());
                DeactivateBuilder();
            }
        }

        private IEnumerator BuildIsland()
        {
            float elapsedTime = 0f;
            while (elapsedTime < ISLAND_UPSCALE_TIME)
            {
                float t = elapsedTime / ISLAND_UPSCALE_TIME;
                _island.transform.localScale = Vector3.Lerp(Vector3.zero, _islandStartScale * ISLAND_UPSCALE_SIZE, t);
                elapsedTime += DELAY_BEFORE_SCALE_ISLAND;
                yield return new WaitForSeconds(DELAY_BEFORE_SCALE_ISLAND);
            }
            yield return new WaitForSeconds(TIME_IN_UPSCALE);
            elapsedTime = 0f;

            while (elapsedTime < ISLAND_DOWN_SCALE_TIME)
            {
                float t = elapsedTime / ISLAND_DOWN_SCALE_TIME;
                _island.transform.localScale = Vector3.Lerp(_islandStartScale * ISLAND_UPSCALE_SIZE, _islandStartScale, t);
                elapsedTime += DELAY_BEFORE_SCALE_ISLAND;
                yield return new WaitForSeconds(DELAY_BEFORE_SCALE_ISLAND);
            }
        }

        private void SpawnResource(ObjectPooler.ObjectInfo.ObjectTypes type)
        {
            var res = ObjectPooler.Instance.GetObject(type, transform.position);
            res.GetComponent<ResourceObjectMovement>().InstantiateFromCharacter(_island.transform);
        }

        public void DeactivateBuilder()
        {
            foreach (IslandBuilder builder in _nextIslandsBuilder)
            {
                builder.gameObject.SetActive(true);
            }

            _resourcePanelContainer.SetActive(false);
            _island.SetActive(true);
            _collider.enabled = false;
            _buildZone.enabled = false;

            if (_takeResource != null)
            {
                StopCoroutine(_takeResource);
            }
        }
    }
}
