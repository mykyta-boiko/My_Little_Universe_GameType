using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ObjectPool;
using PlayerLogic;

namespace Resource
{ 
    public class ResourceDeposits : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _partContainer;
        [SerializeField] private Collider _collider;
        [SerializeField] private List<GameObject> _partsOfResources;
        [SerializeField] private ResourceGatheringViewer _takeLogMessage;

        [Space(5)]
        [Header("Settings")]
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private ObjectPooler.ObjectInfo.ObjectTypes _hitEffect;
        [SerializeField] private ObjectPooler.ObjectInfo.ObjectTypes _flowResourceEffect;
        [SerializeField] private float _timeToRemainingResource = 5f;
        [SerializeField] private float _growthRateTime = 0.8f;


        private Player _player;
        private int _startResourseAmount;
        private int _resourseAmount;
        private bool _isHaveResourse = true;

        // Shake 
        private const float SHAKE_MAGNITUDE = 0.1f;
        private const float SHAKE_DELAY = 0.025f;
        private const int SHAKE_CIRCLE = 5;
        private Vector3 _startPartsPosition;

        // Parts
        private const float DELAY_BEFORE_SCALE_OBJECT = 0.01f;
        private int _hitCount = 0;
        private Vector3 _startPartsScale;
        private int _hitToDisablePart = 2;


        private void Start()
        {
            _startPartsScale = _partsOfResources[0].transform.localScale;
            _startPartsPosition = _partContainer.localPosition;
            _startResourseAmount = _partsOfResources.Count;
            _resourseAmount = _startResourseAmount;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_player == null)
            {
                _player = other.GetComponent<Player>();
            }
            _player.AddTarget(this, _resourceType);
        }

        private void OnTriggerExit(Collider other)
        {
            _player.AddTargetToRemove(this);
        }

        private IEnumerator RestoreResource()
        {
            yield return new WaitForSeconds(_timeToRemainingResource);

            foreach(GameObject partOfResourse in _partsOfResources)
            {
                partOfResourse.SetActive(true);
                partOfResourse.transform.localScale = Vector3.zero;
            }

            float elapsedTime = 0f;

            while (elapsedTime < _growthRateTime)
            {
                float t = elapsedTime / _growthRateTime;
                for (int i = 0; i < _partsOfResources.Count; i++)
                {
                    _partsOfResources[i].transform.localScale = Vector3.Lerp(Vector3.zero, _startPartsScale, t);
                }

                elapsedTime += DELAY_BEFORE_SCALE_OBJECT;
                yield return new WaitForSeconds(DELAY_BEFORE_SCALE_OBJECT);
            }
            _hitCount = 0;
            _resourseAmount = _startResourseAmount;
            _collider.enabled = true;
            _isHaveResourse = true;
        }

        private void SpawnResource()
        {
            var resource = ObjectPooler.Instance.GetObject(_flowResourceEffect, transform.position);
            resource.GetComponent<ResourceObjectMovement>().InstantiateFromResource(_player.transform);
            _takeLogMessage.ShowAddedResource();
        }

        private IEnumerator Shake()
        {
            for (int i = SHAKE_CIRCLE; i > 0; i--)
            {
                float offsetX = Random.Range(-1f, 1f) * SHAKE_MAGNITUDE;
                float offsetY = Random.Range(-1f, 1f) * SHAKE_MAGNITUDE;

                _partContainer.localPosition = new Vector3(_startPartsPosition.x + offsetX, _startPartsPosition.y + offsetY, _startPartsPosition.z);
                yield return new WaitForSeconds(SHAKE_DELAY);
            }
            _partContainer.localPosition = _startPartsPosition;
        }

        public void TakeHit()
        {
            if (_isHaveResourse)
            {
                _hitCount++;
                _player.TakeResource(_resourceType);
                SpawnResource();
                ObjectPooler.Instance.GetObject(_hitEffect, transform.position);
                StartCoroutine(Shake());

                if (_hitCount % _hitToDisablePart == 0)
                {
                    _resourseAmount--;
                    _partsOfResources[_resourseAmount].SetActive(false);
                    if (_resourseAmount == 0)
                    {
                        _collider.enabled = false;
                        _isHaveResourse = false;
                        _player.AddTargetToRemove(this);
                        StartCoroutine(RestoreResource());
                    }
                }
            }
        }
    }
}
