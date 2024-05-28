using TMPro;
using UnityEngine;

namespace Resource
{
    public class ResourceRequirementsPanel : MonoBehaviour
    {
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private TMP_Text _spendedResource;
        [SerializeField] private TMP_Text _requireResources;
        [SerializeField] private string _cameraTag = "MainCamera";

        private const string SING = "/";
        private ResourceType _panelType;

        private void Start()
        {
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindWithTag(_cameraTag).transform;
            }
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _mainCamera.transform.position); // look at the camera
        }

        public void SetStartView(int currentResoucesAmount, int requireResources, ResourceType type)
        {
            _spendedResource.text = currentResoucesAmount.ToString();
            _requireResources.text = SING + requireResources.ToString();
            _panelType = type;
        }

        public void UpdateView(int spendedResource)
        {
            _spendedResource.text = spendedResource.ToString();
        }

        public bool TryDeactivate(ResourceType type)
        {
            if(type == _panelType)
            {
                return true;
            }
            return false;
        }
    }
}