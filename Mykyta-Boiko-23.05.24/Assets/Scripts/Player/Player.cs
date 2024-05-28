using System.Collections.Generic;
using UnityEngine;
using Resource;
using Data;

namespace PlayerLogic
{
    public class Player : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private InventoryController _resourceContorller;
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private GameObject _axeTool;
        [SerializeField] private GameObject _crutchTool;

        [Space(5)]
        [Header("Settigns")]
        [SerializeField] private string _inventoryTag = "Inventory";
        [SerializeField] private float _moveSpeed;


        private List<ResourceDeposits> _activeTargets = new List<ResourceDeposits>();
        private List<ResourceDeposits> _unactiveTargets = new List<ResourceDeposits>();

        private void Start()
        {
            transform.position = DataController.GetCharacterPosition();
            if (_resourceContorller == null)
            {
                _resourceContorller = GameObject.FindGameObjectWithTag(_inventoryTag).GetComponent<InventoryController>();
            }
        }
        private void OnApplicationQuit()
        {
            DataController.SaveCharacterPosition(transform);
        }

        private void Attack()
        {
            foreach (ResourceDeposits target in _activeTargets)
            {
                target.TakeHit();
            }
            CleanupUnactiveTargets();
        }

        private void PickInstrument(ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Wood:
                    _axeTool.SetActive(true);
                    break;
                case ResourceType.Stone:
                    _crutchTool.SetActive(true);
                    break;
                case ResourceType.Crystal:
                    _crutchTool.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        private void CleanupUnactiveTargets()
        {
            for (int i = _unactiveTargets.Count - 1; i >= 0; i--)
            {
                _activeTargets.Remove(_unactiveTargets[i]);
                _unactiveTargets.RemoveAt(i);
            }
            if (_activeTargets.Count == 0)
            {
                _axeTool.SetActive(false);
                _crutchTool.SetActive(false);
                _animator.StopAttack();
            }
        }


        public void AddTarget(ResourceDeposits target, ResourceType resourceType)
        {
            PickInstrument(resourceType);
            if (!_activeTargets.Contains(target))
            {
                _activeTargets.Add(target);
            }
            _animator.StartAttack(Attack);
        }

        public void AddTargetToRemove(ResourceDeposits target)
        {
            _unactiveTargets.Add(target);
        }

        public void TakeResource(ResourceType resource)
        {
            _resourceContorller.AddResourse(resource);
        }
        public bool ExchangeResource(ResourceType resource, int price)
        {
            return _resourceContorller.ExchangeResource(resource, price);
        }

        public bool BuildIsland(ResourceType type)
        {
            return _resourceContorller.WasteResourse(type);
        }
    }
}