using System.Collections;
using UnityEngine;
using ObjectPool;

namespace Resource
{
    public class ResourceObjectMovement : MonoBehaviour, IPooled
    {
        [SerializeField] private ObjectPooler.ObjectInfo.ObjectTypes _type;
        [SerializeField] private float _resourcePushSpeed = 0.5f;
        [SerializeField] private float _pushDistance = 1.2f;
        [SerializeField] private float _pushToBuildDistance = 0.6f;
        [SerializeField] private float _upPushDistance = 2f;
        [SerializeField] private float _speedToCharacter = 3f;
        [SerializeField] private float _delayBeforeStartFly = 1f;

        private const float MAX_ROTATION = 180;
        private const float MIN_ROTATION = 60;
        private const float CHARACTER_OFFSET = 1f;

        private Transform _movePosition;
        private Vector3 _offset;
        private Vector3 _rotation;
        private Vector3 _pushPoint;
        private bool _isCanMoveToObject = false;

        public ObjectPooler.ObjectInfo.ObjectTypes Type => _type;

        private IEnumerator StartFly()
        {
            yield return new WaitForSeconds(_delayBeforeStartFly);
            _isCanMoveToObject = true;
        }

        private void Update()
        {
            MoveToObject();
        }

        private void MoveToObject()
        {
            if (_isCanMoveToObject)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    _movePosition.position + _offset, Time.deltaTime * _speedToCharacter);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    _pushPoint, Time.deltaTime * _resourcePushSpeed);

                transform.Rotate(_rotation * Time.deltaTime);
            }

            if (transform.position == _movePosition.position + _offset)
            {
                _isCanMoveToObject = false;
                ObjectPooler.Instance.DestroyObject(gameObject);
            }
        }

        public void InstantiateFromResource(Transform positionToMove)
        {
            _offset = Vector3.zero;
            _offset.y += CHARACTER_OFFSET;
            _movePosition = positionToMove;

            float xPosition = Random.Range(-_pushDistance, _pushDistance) + transform.position.x;
            float yPositon = transform.position.y + _pushDistance;
            float zPosition = Random.Range(-_pushDistance, _pushDistance) + transform.position.z;
            _pushPoint = new Vector3(xPosition, yPositon, zPosition);

            float rotation = Random.Range(MIN_ROTATION, MAX_ROTATION) + transform.rotation.x;
            _rotation = new Vector3(rotation, rotation, rotation);

            StartCoroutine(StartFly());
        }

        public void InstantiateFromCharacter(Transform positionToMove)
        {
            _offset = Vector3.zero;
            _movePosition = positionToMove;

            float xPosition = Random.Range(-_pushToBuildDistance, _pushToBuildDistance) + positionToMove.position.x;
            float yPositon = positionToMove.position.y + _upPushDistance;
            float zPosition = Random.Range(-_pushToBuildDistance, _pushToBuildDistance) + positionToMove.position.z;
            _pushPoint = new Vector3(xPosition, yPositon, zPosition);

            float rotation = Random.Range(MIN_ROTATION, MAX_ROTATION) + transform.rotation.x;
            _rotation = new Vector3(rotation, rotation, rotation);

            StartCoroutine(StartFly());
        }
    }
}