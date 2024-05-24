using System.Collections.Generic;
using UnityEngine;
using System;
using ResourceLogic;

namespace ObjectPool
{
    public class ObjectPooler : MonoBehaviour
    {

        public static ObjectPooler Instance;
        [Serializable]
        public struct ObjectInfo
        {
            public ResourceType Type;
            public GameObject Prefab;
            public int StartCount;
        }

        [SerializeField] private List<ObjectInfo> _objectsInfo;
        private Dictionary<ResourceType, Pool> _objectPools;
        private void Awake()
        {
            if (Instance == null) Instance = this;

            InitPool();
        }
        private void InitPool()
        {
            _objectPools = new Dictionary<ResourceType, Pool>();
            var emptyGO = new GameObject();

            foreach (var obj in _objectsInfo)
            {
                var container = Instantiate(emptyGO, transform, false);
                container.name = obj.Type.ToString();

                _objectPools[obj.Type] = new Pool(container.transform);

                for (int i = 0; i < obj.StartCount; i++)
                {
                    var go = InstantiateObject(obj.Type, container.transform);
                    _objectPools[obj.Type].Objects.Enqueue(go);
                }
            }
            Destroy(emptyGO);
        }
        private GameObject InstantiateObject(ResourceType type, Transform parent)
        {
            var go = Instantiate(_objectsInfo.Find(x => x.Type == type).Prefab, parent);
            go.SetActive(false);
            return go;
        }

        public GameObject GetAndSetEffectObject(ResourceType type, Vector3 position, Quaternion rotation)
        {
            var obj = _objectPools[type].Objects.Count > 0 ?
                _objectPools[type].Objects.Dequeue() : InstantiateObject(type, _objectPools[type].Container);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }
        public GameObject GetEffectObject(ResourceType type, Vector3 position)
        {
            var obj = _objectPools[type].Objects.Count > 0 ?
                _objectPools[type].Objects.Dequeue() : InstantiateObject(type, _objectPools[type].Container);
            obj.transform.position = position;
            obj.SetActive(true);
            return obj;
        }
        public void DestroyEffectObject(GameObject obj)
        {
            _objectPools[obj.GetComponent<IPooled>().Type].Objects.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}

