using TMPro;
using UnityEngine;

namespace Resource
{
    public class ResourceRequirementsPanel : CameraPositionWatcher
    {
        [SerializeField] private TMP_Text _spendedResource;
        [SerializeField] private TMP_Text _requireResources;

        private const string SING = "/";
        private ResourceType _panelType;

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