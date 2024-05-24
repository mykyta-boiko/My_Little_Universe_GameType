using UnityEngine;
using System;
using System.Collections;

namespace IslandLogic
{
    public class Island : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private IslandTypes _islandType;
        [SerializeField] private int _startResourseAmount = 3;
        [SerializeField] private float _timeToRemainingResourse = 5f;
        private int _hitToTakeResourse = 2;
        private int _hitCount = 0;
        private int _resourseAmount;
        private bool _isHaveResourse = true;
        private Player _player;

        private void Start()
        {
            _resourseAmount = _startResourseAmount;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_player == null)
            {
                _player = other.GetComponent<Player>();
            }
            _player.AddTarget(gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            _player.RemoveTarget(gameObject);
        }
        private IEnumerator RestoreResourse()
        {
            yield return new WaitForSeconds(_timeToRemainingResourse);
            _hitCount = 0;
            _resourseAmount = _startResourseAmount;
            _collider.enabled = true;
            _isHaveResourse = true;
        }

        public void TakeHit()
        {
            if (_isHaveResourse)
            {
                _hitCount++;
                StopCoroutine(RestoreResourse());
                if (_hitCount % _hitToTakeResourse == 0)
                {
                    _player.TakeResourse(_islandType);
                    if (_resourseAmount == 0)
                    {
                        StartCoroutine(RestoreResourse());
                        _collider.enabled = false;
                        _isHaveResourse = false;
                    }
                }
            }
        }

        

    }
}
