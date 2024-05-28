using UnityEngine;

namespace Data
{
    public static class DataController
    {
        public static void SaveLastBuiltIslandName(string islandName)
        {
            PlayerPrefs.SetString(PlayerPrefsKeyContainer.GetLastBuiltIslandName(), islandName);
        }

        public static void SaveCharacterPosition(Transform characterPosition)
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeyContainer.GetCharacterPositionX(), characterPosition.position.x);
            PlayerPrefs.SetFloat(PlayerPrefsKeyContainer.GetCharacterPositionY(), characterPosition.position.y);
            PlayerPrefs.SetFloat(PlayerPrefsKeyContainer.GetCharacterPositionZ(), characterPosition.position.z);
        }
        public static Vector3 GetCharacterPosition()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKeyContainer.GetCharacterPositionX()))
            {
                float positionX = PlayerPrefs.GetFloat(PlayerPrefsKeyContainer.GetCharacterPositionX());
                float positionY = PlayerPrefs.GetFloat(PlayerPrefsKeyContainer.GetCharacterPositionY());
                float positionZ = PlayerPrefs.GetFloat(PlayerPrefsKeyContainer.GetCharacterPositionZ());
                return new Vector3(positionX, positionY, positionZ);
            }
            return new Vector3(0, 0, 0);
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

        public static void SaveCraftingResource(string resourceType, int resourceAmount)
        {
            string saveKey = PlayerPrefsKeyContainer.GetCraftingResourceKey() + resourceType.ToString();
            PlayerPrefs.SetInt(saveKey, resourceAmount);
        }

        public static int GetCraftingResource(string resourceType)
        {
            string saveKey = PlayerPrefsKeyContainer.GetCraftingResourceKey() + resourceType.ToString();
            if(PlayerPrefs.HasKey(saveKey))
            {
                return PlayerPrefs.GetInt(saveKey);
            }
            return 0;
        }
    }
}
