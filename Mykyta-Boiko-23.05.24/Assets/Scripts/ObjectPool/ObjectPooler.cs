using System.Collections.Generic;
using UnityEngine;
using System;

namespace ObjectPool
{
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler Instance;
        [Serializable]
        public struct ObjectInfo
        {
            public enum ObjectTypes
            {
                FlyingLog,
                FlyingStone,
                FlyingCrystal,
                TreesHit,
                RockHit,
                CrystalHit,
                SpendResource,
                TakeLogMessage,
                FlyingLumber,
                FlyingBrick
            }
            public GameObject Prefab;
            public int StartCount;
            public ObjectTypes ObjectType;
            public Canvas ParentCanvas;
        }

        [SerializeField] private List<ObjectInfo> _objectsInfo;
        private Dictionary<ObjectInfo.ObjectTypes, Pool> _objectPools;
        private void Awake()
        {
            if (Instance == null) Instance = this;

            InitPool();
        }
        private void InitPool()
        {
            _objectPools = new Dictionary<ObjectInfo.ObjectTypes, Pool>();
            var emptyGameObject = new GameObject();

            foreach (var objectInfo in _objectsInfo)
            {
                Transform parent = objectInfo.ParentCanvas != null ? objectInfo.ParentCanvas.transform : transform;
                var container = Instantiate(emptyGameObject, parent, false);
                container.name = objectInfo.ObjectType.ToString();

                _objectPools[objectInfo.ObjectType] = new Pool(container.transform);

                for (int i = 0; i < objectInfo.StartCount; i++)
                {
                    var gameObject = InstantiateObject(objectInfo.ObjectType, container.transform);
                    _objectPools[objectInfo.ObjectType].Objects.Enqueue(gameObject);
                }
            }
            Destroy(emptyGameObject);
        }
        private GameObject InstantiateObject(ObjectInfo.ObjectTypes type, Transform parent)
        {
            var objectInfo = _objectsInfo.Find(x => x.ObjectType == type);
            Transform finalParent = objectInfo.ParentCanvas != null ? objectInfo.ParentCanvas.transform : parent;
            var gameobject = Instantiate(_objectsInfo.Find(x => x.ObjectType == type).Prefab, parent);
            gameobject.SetActive(false);
            return gameobject;
        }

        public GameObject GetObject(ObjectInfo.ObjectTypes type, Vector3 position, Quaternion rotation)
        {
            var obj = _objectPools[type].Objects.Count > 0 ?
                _objectPools[type].Objects.Dequeue() : InstantiateObject(type, _objectPools[type].Container);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
        public GameObject GetObject(ObjectInfo.ObjectTypes type, Vector3 position)
        {
            var obj = _objectPools[type].Objects.Count > 0 ?
                _objectPools[type].Objects.Dequeue() : InstantiateObject(type, _objectPools[type].Container);
            obj.transform.position = position;
            obj.SetActive(true);
            return obj;
        }
        public void DestroyObject(GameObject obj)
        {
            _objectPools[obj.GetComponent<IPooled>().Type].Objects.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}

