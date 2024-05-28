using UnityEngine;
using Resource;
using System;
using Data;

namespace PlayerLogic
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryViewer _resourceView;

        private int _woodAmount;
        private int _stoneAmount;
        private int _crystalAmount;
        private int _lumberAmount;
        private int _brickAmount;

        private void Awake()
        {
            DataController.GetResourceData(wood: out _woodAmount, stone: out _stoneAmount, 
                crystal: out _crystalAmount, lumber: out _lumberAmount, brick: out _brickAmount);

            _resourceView.StartGameView(wood: _woodAmount, stone: _stoneAmount, 
                crystal: _crystalAmount, lumber: _lumberAmount, brick: _brickAmount);
        }

        private void OnDestroy()
        {
            DataController.SaveResourceData(wood: _woodAmount, stone: _stoneAmount,
                crystal: _crystalAmount, lumber: _lumberAmount, brick: _brickAmount);
        }


        private void ChangeResourseValue(ref int amount, Action<int> viewUpdate, bool isIncreased)
        {
            amount = isIncreased? amount + 1 : amount - 1;
            viewUpdate?.Invoke(amount);
        }
        private bool TryWasteResourseValue(ref int amount, Action<int> viewUpdate)
        {
            if (amount > 0)
            {
                amount--;
                viewUpdate?.Invoke(amount);
                return true;
            }
            return false;
        }

        public void AddResourse(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Wood:
                    ChangeResourseValue(ref _woodAmount, _resourceView.ChangeWoodView, isIncreased: true);
                    break;
                case ResourceType.Stone:
                    ChangeResourseValue(ref _stoneAmount, _resourceView.ChangeStoneView, isIncreased: true);
                    break;
                case ResourceType.Crystal:
                    ChangeResourseValue(ref _crystalAmount, _resourceView.ChangeCrystalView, isIncreased: true);
                    break;
                case ResourceType.Lumber:
                    ChangeResourseValue(ref _lumberAmount, _resourceView.ChangeLumberView, isIncreased: true);
                    break;
                case ResourceType.Brick:
                    ChangeResourseValue(ref _brickAmount, _resourceView.ChangeBrickView, isIncreased: true);
                    break;
                default:
                    break;
            }
        }

        public bool ExchangeResource(ResourceType type, int changePrice)
        {
            switch (type)
            {
                case ResourceType.Wood:
                    if (_woodAmount >= changePrice)
                    {
                        _woodAmount -= changePrice;
                        _resourceView.ChangeWoodView(_woodAmount);
                        return true;
                    }
                    return false;

                case ResourceType.Stone:
                    if (_stoneAmount >= changePrice)
                    {
                        _stoneAmount -= changePrice;
                        _resourceView.ChangeStoneView(_stoneAmount);
                        return true;
                    }
                    return false;

                default: 
                    return false;
            }
        }

        public bool WasteResourse(ResourceType type)
        {
            return type switch
            {
                ResourceType.Wood => TryWasteResourseValue(ref _woodAmount, _resourceView.ChangeWoodView),
                ResourceType.Stone => TryWasteResourseValue(ref _stoneAmount, _resourceView.ChangeStoneView),
                ResourceType.Crystal => TryWasteResourseValue(ref _crystalAmount, _resourceView.ChangeCrystalView),
                ResourceType.Lumber => TryWasteResourseValue(ref _lumberAmount, _resourceView.ChangeLumberView),
                ResourceType.Brick => TryWasteResourseValue(ref _brickAmount, _resourceView.ChangeBrickView),
                _ => false,
            };
        }

    }
}