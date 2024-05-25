using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace IslandLogic
{
    public class IslandController : MonoBehaviour
    {
        [SerializeField] private List<IslandBuilder> _islandToActivate;

        private void Awake()
        {
            string lastBuiltIslandName = DataController.GetLastBuiltIslandName();
            if (lastBuiltIslandName != null)
            {
                for (int i = 0; i < _islandToActivate.Count; i++)
                {
                    _islandToActivate[i].DeactivateBuilder();
                    if (_islandToActivate[i].gameObject.name == lastBuiltIslandName)
                    {
                        break;
                    }
                }
            }
        }
    }
}