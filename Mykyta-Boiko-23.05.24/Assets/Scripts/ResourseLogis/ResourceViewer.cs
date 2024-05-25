using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ResourceLogic
{
    public class ResourceViewer : MonoBehaviour
    {
        [Header("Resourse Panels")]
        [SerializeField] private GameObject _woodPanel;
        [SerializeField] private GameObject _stonePanel;
        [SerializeField] private GameObject _crystalPanel;
        [SerializeField] private GameObject _lumberPanel;
        [SerializeField] private GameObject _brickPanel;

        [Space(5)]
        [Header("Resourse Text")]
        [SerializeField] TMP_Text _woodAmountText;
        [SerializeField] TMP_Text _stoneAmountText;
        [SerializeField] TMP_Text _crystalAmountText;
        [SerializeField] TMP_Text _lumberAmountText;
        [SerializeField] TMP_Text _brickAmountText;

        public void StartGameView(int wood, int stone, int crystal, int lumber, int brick)
        {
            ChangeWoodView(wood);
            ChangeStoneView(stone);
            ChangeCrystalView(crystal);
            ChangeLumberView(lumber);
            ChangeBrickView(brick);
        }

        public void ChangeWoodView(int value)
        {
            _woodAmountText.text = value.ToString();
        }

        public void ChangeStoneView(int value)
        {
            _stoneAmountText.text = value.ToString();
            if (value == 0)
            {
                _stonePanel.SetActive(false);
            }
            else
            {
                _stonePanel.SetActive(true);
            }
        }

        public void ChangeCrystalView(int value)
        {
            _crystalAmountText.text = value.ToString();
            if (value == 0)
            {
                _crystalPanel.SetActive(false);
            }
            else
            {
                _crystalPanel.SetActive(true);
            }
        }

        public void ChangeLumberView(int value)
        {
            _lumberAmountText.text = value.ToString();
            if (value == 0)
            {
                _lumberPanel.SetActive(false);
            }
            else
            {
                _lumberPanel.SetActive(true);
            }
        }

        public void ChangeBrickView(int value)
        {
            _brickAmountText.text = value.ToString();
            if (value == 0)
            {
                _brickPanel.SetActive(false);
            }
            else
            {
                _brickPanel.SetActive(true);
            }
        }
    }
}
