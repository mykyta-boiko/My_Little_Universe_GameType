using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Data;
using ObjectPool;
using PlayerLogic;

namespace Resource
{
    public class ResourceCrafter : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TMP_Text _textPrice;
        [SerializeField] private TMP_Text _craftingQueueText;
        [SerializeField] private Slider _craftingProcessSlider;
        [SerializeField] private Transform _pile;

        [Space(5)]
        [Header("Settings")]
        [SerializeField] private ObjectPooler.ObjectInfo.ObjectTypes _spendResourceEffect;
        [SerializeField] private ObjectPooler.ObjectInfo.ObjectTypes _spawnedResources;
        [SerializeField] private ResourceType _spendResource;
        [SerializeField] private ResourceType _craftingResource;
        [SerializeField] private int _resourceCost = 5;
        [SerializeField] private float _craftingTime = 1f;

        [Space(5)]
        [Header("Other")]
        [SerializeField] private Player _player;

        private Coroutine _prepareResourceToExchange;
        private const float DELAY_BEFORE_NEXT_CHANGE = 0.2f;
        private int _craftringQueueAmount = 0;
        private bool _playerCanExchange = false;

        private void Start()
        {
            _textPrice.text = _resourceCost.ToString();
            _craftringQueueAmount = DataController.GetCraftingResource(_craftingResource.ToString());

            if (_craftringQueueAmount > 0)
            {
                StartCraftingResource();
                if (_player == null)
                {
                    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(StartEchange());
        }

        private void OnTriggerExit()
        {
            _playerCanExchange = false;
        }

        private void OnApplicationQuit()
        {
            DataController.SaveCraftingResource(_craftingResource.ToString(), _craftringQueueAmount);
        }

        private void StartCraftingResource()
        {
            _craftingProcessSlider.gameObject.SetActive(true);
            _craftingQueueText.gameObject.SetActive(true);
            _craftingQueueText.text = _craftringQueueAmount.ToString();
            if (_prepareResourceToExchange == null)
            {
                _prepareResourceToExchange = StartCoroutine(PrepareCraftingResource());
            }
        }

        private IEnumerator StartEchange()
        {
            _playerCanExchange = true;
            while (_playerCanExchange)
            {
                if (_player.ExchangeResource(_spendResource, _resourceCost))
                {
                    _craftringQueueAmount++;
                    StartCraftingResource();
                    ObjectPooler.Instance.GetObject(_spendResourceEffect, transform.position, transform.rotation);
                    SpawnResource();
                }
                yield return new WaitForSeconds(DELAY_BEFORE_NEXT_CHANGE);
            }
        }

        private void SpawnResource()
        {
            for (int i = 0; i < _resourceCost; i++)
            {
                var res = ObjectPooler.Instance.GetObject(_spawnedResources, _player.transform.position);
                res.GetComponent<ResourceObjectMovement>().InstantiateFromCharacter(_pile.transform);
            }
        }

        private IEnumerator PrepareCraftingResource()
        {
            float elapsedTime = 0f;
            _craftingProcessSlider.value = 0f;

            while (elapsedTime < _craftingTime)
            {
                _craftingProcessSlider.value = Mathf.Clamp01(elapsedTime / _craftingTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _craftringQueueAmount--;
            _craftingQueueText.text = _craftringQueueAmount.ToString();
            _player.TakeResource(_craftingResource);
            _craftingProcessSlider.value = 1f;

            if (_craftringQueueAmount > 0)
            {
                StartCoroutine(PrepareCraftingResource());
            }
            else
            {
                _craftingProcessSlider.gameObject.SetActive(false);
                _craftingQueueText.gameObject.SetActive(false);
                _prepareResourceToExchange = null;
            }
        }
    }
}
