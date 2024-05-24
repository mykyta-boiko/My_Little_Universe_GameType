using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using IslandLogic;

namespace ResourceLogic
{
    public class ResourceController : MonoBehaviour
    {
        [Header("Wood Resourse Panels")]
        [SerializeField] private GameObject _woodPanel;
        [SerializeField] private GameObject _boardPanel;
        [SerializeField] TMP_Text _woodAmountText;
        [SerializeField] TMP_Text _boardAmountText;

        [Space(5)]
        [Header("Stone Resourse Panels")]
        [SerializeField] private GameObject _stonePanel;
        [SerializeField] private GameObject _processedStonePanel;
        [SerializeField] TMP_Text _stoneAmountText;
        [SerializeField] TMP_Text _processedStoneAmountText;

        [Space(5)]
        [Header("Crystal Resourse Panels")]
        [SerializeField] private GameObject _processedCrystalPanel;
        [SerializeField] private GameObject _crystalPanel;
        [SerializeField] TMP_Text _crystalAmountText;
        [SerializeField] TMP_Text _processedCrystalAmountText;

        private int _woodAmount = 0;
        private int _boardAmount = 0;

        private int _stoneAmount = 0;
        private int _processedStoneAmount = 0;

        private int _crystalAmount = 0;
        private int _processedCrystalAmount = 0;

        private void ChangeResourseValue(ref int amount, TMP_Text text, bool isIncreased)
        {
            amount = isIncreased? amount + 1 : amount - 1;
            text.text = amount.ToString();
        }
        private bool TryWasteResourseValue(ref int amount, TMP_Text text)
        {
            if (amount > 0)
            {
                amount--;
                text.text = amount.ToString();
                return true;
            }
            return false;
        }

        public void AddResourse(IslandTypes type)
        {
            switch (type)
            {
                case IslandTypes.Wood:
                    ChangeResourseValue(ref _woodAmount, _woodAmountText, isIncreased: true);
                    break;
                case IslandTypes.Rock:
                    ChangeResourseValue(ref _stoneAmount, _stoneAmountText, isIncreased: true);
                    break;
                case IslandTypes.Crystal:
                    ChangeResourseValue(ref _crystalAmount, _crystalAmountText, isIncreased: true);
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
                        ChangeResourseValue(ref _woodAmount, _woodAmountText, isIncreased: false);
                        ChangeResourseValue(ref _boardAmount, _boardAmountText, isIncreased: true);
                    }
                    break;
                case IslandTypes.Rock:
                    if (_stoneAmount > 0)
                    {
                        ChangeResourseValue(ref _stoneAmount, _stoneAmountText, isIncreased: false);
                        ChangeResourseValue(ref _processedStoneAmount, _processedStoneAmountText, isIncreased: true);
                    }
                    break;
                case IslandTypes.Crystal:
                    if (_crystalAmount > 0)
                    {
                        ChangeResourseValue(ref _crystalAmount, _crystalAmountText, isIncreased: false);
                        ChangeResourseValue(ref _processedCrystalAmount, _processedCrystalAmountText, isIncreased: true);
                    }
                    break;
                default:
                    break;
            }
        }

        public bool WasteResourse(ResourceType type)
        {
            return type switch
            {
                ResourceType.Wood => TryWasteResourseValue(ref _woodAmount, _woodAmountText),
                ResourceType.Board => TryWasteResourseValue(ref _boardAmount, _boardAmountText),
                ResourceType.Stone => TryWasteResourseValue(ref _stoneAmount, _stoneAmountText),
                ResourceType.ProcessedStone => TryWasteResourseValue(ref _processedStoneAmount, _processedStoneAmountText),
                ResourceType.Crystal => TryWasteResourseValue(ref _crystalAmount, _crystalAmountText),
                ResourceType.ProcessedCrystal => TryWasteResourseValue(ref _processedCrystalAmount, _processedCrystalAmountText),
                _ => false,
            };
        }

    }
}