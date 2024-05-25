using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using IslandLogic;
using Data;
using System;

namespace ResourceLogic
{
    public class ResourceController : MonoBehaviour
    {
        [SerializeField] private ResourceViewer _resourceView;

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

        public void AddResourse(IslandTypes type)
        {
            switch (type)
            {
                case IslandTypes.Wood:
                    ChangeResourseValue(ref _woodAmount, _resourceView.ChangeWoodView, isIncreased: true);
                    break;
                case IslandTypes.Stone:
                    ChangeResourseValue(ref _stoneAmount, _resourceView.ChangeStoneView, isIncreased: true);
                    break;
                case IslandTypes.Crystal:
                    ChangeResourseValue(ref _crystalAmount, _resourceView.ChangeCrystalView, isIncreased: true);
                    break;
                default:
                    break;
            }
        }

        public void ChangeResource(IslandTypes type)
        {
            switch (type)
            {
                case IslandTypes.Wood:
                    if (_woodAmount > 0)
                    {
                        ChangeResourseValue(ref _woodAmount, _resourceView.ChangeWoodView, isIncreased: false);
                        ChangeResourseValue(ref _lumberAmount, _resourceView.ChangeLumberView, isIncreased: true);
                    }
                    break;
                case IslandTypes.Stone:
                    if (_stoneAmount > 0)
                    {
                        ChangeResourseValue(ref _stoneAmount, _resourceView.ChangeStoneView, isIncreased: false);
                        ChangeResourseValue(ref _brickAmount, _resourceView.ChangeBrickView, isIncreased: true);
                    }
                    break;
/*                case IslandTypes.Crystal:
                    if (_crystalAmount > 0)
                    {
                        ChangeResourseValue(ref _crystalAmount, _resourceView.ChangeCrystalView, isIncreased: false);
                        ChangeResourseValue(ref _processedCrystalAmount, _processedCrystalAmountText, isIncreased: true);
                    }
                    break;*/
                default:
                    break;
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