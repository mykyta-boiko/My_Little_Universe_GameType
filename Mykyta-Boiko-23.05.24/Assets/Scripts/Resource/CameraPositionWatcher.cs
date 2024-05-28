using UnityEngine;

namespace Resource
{ 
    public class CameraPositionWatcher : MonoBehaviour
    {
        [SerializeField] protected Transform _mainCamera;
        [SerializeField] protected string _cameraTag = "MainCamera";
    
        protected virtual void Start()
        {
            FindCamera();
        }

        protected virtual void FindCamera()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindWithTag(_cameraTag).transform;
            }
        }
    
        protected virtual void Update()
        {
            RotateSelftToCamera();
        }

        protected virtual void RotateSelftToCamera()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _mainCamera.transform.position);
        }
    }
}