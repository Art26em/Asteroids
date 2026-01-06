using System.IO;
using UnityEngine;

namespace Core.Configs
{
    public class PlayerConfigLoader
    {
        private const string DefaultFileName = "playerConfig.json";
        private const string DefaultDirectoryName = "Resources";
        private PlayerData _defaultPlayerData;

        public PlayerConfigLoader(PlayerData defaultPlayerData)
        {
            _defaultPlayerData = defaultPlayerData;
        }
        
        private string GetFilePath()
        {
            return Path.Combine(
                Application.persistentDataPath,
                DefaultDirectoryName, 
                DefaultFileName);
        }
        
        public PlayerData LoadConfigs()
        {
            var filePath = GetFilePath();

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var settings = JsonUtility.FromJson<PlayerData>(json);
                return settings;
            }

            SaveConfigs(_defaultPlayerData);
            return _defaultPlayerData;

        }

        private void SaveConfigs(PlayerData playerData)
        {
            var filePath = GetFilePath();
            var directoryName = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryName))
            {
                if (directoryName != null) Directory.CreateDirectory(directoryName);
            }
            var json = JsonUtility.ToJson(playerData);
            File.WriteAllText(filePath, json);
        }
        
    }
}