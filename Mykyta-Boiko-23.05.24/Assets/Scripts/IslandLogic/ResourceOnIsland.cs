using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace IslandLogic
{
    public class ResourceOnIsland : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private IslandTypes _islandType;
        [SerializeField] private float _timeToRemainingResourse = 5f;
        [SerializeField] private List<GameObject> _partsOfResources;
        private int _startResourseAmount;

        private int _hitToTakeResourse = 2;
        private int _hitCount = 0;
        private int _resourseAmount;
        private bool _isHaveResourse = true;
        private Player _player;

        private void Start()
        {
            _startResourseAmount = _partsOfResources.Count;
            _resourseAmount = _startResourseAmount;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_player == null)
            {
                _player = other.GetComponent<Player>();
            }
            _player.AddTarget(this, _islandType);
        }

        private void OnTriggerExit(Collider other)
        {
            _player.AddTargetToRemove(this);
        }
        private IEnumerator RestoreResourse()
        {
            yield return new WaitForSeconds(_timeToRemainingResourse);
            _hitCount = 0;
            _resourseAmount = _startResourseAmount;
            _collider.enabled = true;
            _isHaveResourse = true;
            foreach(GameObject partOfResourse in _partsOfResources)
            {
                partOfResourse.SetActive(true);
            }
        }

        public void TakeHit()
        {
            if (_isHaveResourse)
            {
                _hitCount++;
                _player.TakeResourse(_islandType);
                StopCoroutine(RestoreResourse());
                if (_hitCount % _hitToTakeResourse == 0)
                {
                    _resourseAmount--;
                    _partsOfResources[_resourseAmount].SetActive(false);
                    if (_resourseAmount == 0)
                    {
                        StartCoroutine(RestoreResourse());
                        _collider.enabled = false;
                        _isHaveResourse = false;
                        _player.AddTargetToRemove(this);
                    }
                }
            }
        }
    }
}
