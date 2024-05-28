using UnityEngine;
using UnityEngine.SceneManagement;
using Data;
using System.Collections;

namespace IslandLogic
{
    public class RestartGamePortal : MonoBehaviour
    {
        [SerializeField] private IslandManager _manager;
        [SerializeField] private string ISLAND_MANAGER_TAG;
        private const float DELAY = 1f;
        private bool _start = false;
        private void Start()
        {
            if(_manager == null)
            {
                _manager = GameObject.FindWithTag(ISLAND_MANAGER_TAG).GetComponent<IslandManager>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_start)
            {
                _start = true;
                StartCoroutine(RestartScene());
            }
        }

        private IEnumerator RestartScene()
        {
            _manager.DeleteIslandStatesFile();
            string currentSceneName = SceneManager.GetActiveScene().name;
            DataController.DeleteData();
            yield return new WaitForSeconds(DELAY);
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
