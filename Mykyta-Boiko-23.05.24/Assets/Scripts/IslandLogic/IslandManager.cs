using IslandLogic;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class IslandManager : MonoBehaviour
{
    private const string FileName = "islands.json";
    [SerializeField] private List<IslandBuilder> _islandBuilders;

    private Dictionary<string, bool> _islandStates = new Dictionary<string, bool>();

    private void Awake()
    {
        LoadIslandStates();
    }

    private void OnApplicationQuit()
    {
        SaveIslandStates();
    }

    private void SaveIslandStates()
    {
        _islandStates.Clear();
        foreach (var builder in _islandBuilders)
        {
            {
                _islandStates[builder.name] = builder.IsBult;
            }
        }

        string json = JsonUtility.ToJson(new Serialization<string, bool>(_islandStates), true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, FileName), json);
    }

    private void LoadIslandStates()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            _islandStates = JsonUtility.FromJson<Serialization<string, bool>>(json).ToDictionary();
            UpdateIslands();
        }
        else
        {
            foreach (var island in _islandBuilders)
            {
                island.gameObject.SetActive(false);
            }
            _islandBuilders[0].gameObject.SetActive(true);
        }
    }

    private void UpdateIslands()
    {
        // hide islands
        foreach(var island in _islandBuilders)
        {
            island.gameObject.SetActive(false);
        }

        // 
        foreach (var state in _islandStates)
        {
            IslandBuilder builder = _islandBuilders.Find(island => island.name == state.Key);
            if (builder != null && state.Value)
            {
                builder.DeactivateBuilder();
            }
        }
        // active firs buildObject 
        _islandBuilders[0].gameObject.SetActive(true);
    }
    public void DeleteIslandStatesFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    [System.Serializable]
    private class Serialization<TKey, TValue>
    {
        public List<TKey> keys;
        public List<TValue> values;

        public Serialization(Dictionary<TKey, TValue> dictionary)
        {
            keys = new List<TKey>(dictionary.Keys);
            values = new List<TValue>(dictionary.Values);
        }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
            for (int i = 0; i < keys.Count; i++)
            {
                dictionary[keys[i]] = values[i];
            }
            return dictionary;
        }
    }
}