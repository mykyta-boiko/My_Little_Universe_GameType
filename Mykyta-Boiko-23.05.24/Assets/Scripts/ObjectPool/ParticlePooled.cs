using System.Collections;
using UnityEngine;

namespace ObjectPool
{
    public class ParticlePooled : MonoBehaviour, IPooled
    {
        [SerializeField] private ObjectPooler.ObjectInfo.ObjectTypes _type;
        [SerializeField] private float _stopAction;

        public ObjectPooler.ObjectInfo.ObjectTypes Type => _type;

        private void OnEnable()
        {
            StartCoroutine(MakeDelayBeforeDestroyObject());
        }

        private IEnumerator MakeDelayBeforeDestroyObject()
        {
            yield return new WaitForSeconds(_stopAction);
            StopAllCoroutines();
            ObjectPooler.Instance.DestroyObject(gameObject);
        }
    }
}