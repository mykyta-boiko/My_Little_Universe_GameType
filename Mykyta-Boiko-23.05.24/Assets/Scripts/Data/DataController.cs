using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Data
{
    public static class DataController
    {
        public static void SaveLastBuiltIslandName(string islandName)
        {
            PlayerPrefs.SetString(PlayerPrefsKeyContainer.GetLastBuiltIslandName(), islandName);
        }

        public static string GetLastBuiltIslandName()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKeyContainer.GetLastBuiltIslandName()))
            {
                return PlayerPrefs.GetString(PlayerPrefsKeyContainer.GetLastBuiltIslandName());
            }
            return null;
        }

        public static void SaveResourceData(int wood, int stone, int crystal, int lumber, int brick)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeyContainer.GetWoodKey(), wood);
            PlayerPrefs.SetInt(PlayerPrefsKeyContainer.GetStoneKey(), stone);
            PlayerPrefs.SetInt(PlayerPrefsKeyContainer.GetCrystalKey(), crystal);
            PlayerPrefs.SetInt(PlayerPrefsKeyContainer.GetLumberKey(), lumber);
            PlayerPrefs.SetInt(PlayerPrefsKeyContainer.GetBrickKey(), brick);
        }
        public static void GetResourceData(out int wood, out int stone, out int crystal, out int lumber, out int brick)
        {
            wood = PlayerPrefs.HasKey(PlayerPrefsKeyContainer.GetWoodKey()) ?
                PlayerPrefs.GetInt(PlayerPrefsKeyContainer.GetWoodKey()) : 0;

            stone = PlayerPrefs.HasKey(PlayerPrefsKeyContainer.GetStoneKey()) ?
                PlayerPrefs.GetInt(PlayerPrefsKeyContainer.GetStoneKey()) : 0;

            crystal = PlayerPrefs.HasKey(PlayerPrefsKeyContainer.GetCrystalKey()) ?
                PlayerPrefs.GetInt(PlayerPrefsKeyContainer.GetCrystalKey()) : 0;

            lumber = PlayerPrefs.HasKey(PlayerPrefsKeyContainer.GetLumberKey()) ?
                PlayerPrefs.GetInt(PlayerPrefsKeyContainer.GetLumberKey()) : 0;

            brick = PlayerPrefs.HasKey(PlayerPrefsKeyContainer.GetStoneKey()) ?
                PlayerPrefs.GetInt(PlayerPrefsKeyContainer.GetBrickKey()) : 0;
        }
    }
}
