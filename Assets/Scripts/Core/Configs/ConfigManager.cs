using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Configs
{
    public class ConfigManager<T> where T : new()
    {
        public T LoadConfigs(string configName)
        {
            var filePath = GetFilePath(configName);

            if (File.Exists(filePath))
            {
                using StreamReader reader = new(filePath);
                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T>(json);
                return data;
            }
            
            var defaultData = new T();
            SaveConfigs(new T());
            return defaultData;
        }

        private void SaveConfigs(T data)
        {
            var filePath = GetFilePath(ConfigsSettings.PlayerConfigName);
            var directoryName = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryName))
            {
                if (directoryName != null) Directory.CreateDirectory(directoryName);
            }
            
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            
            using StreamWriter writer = new(filePath);
            writer.Write(json);
        }
        
        private static string GetFilePath(string configName)
        {
            return Path.Combine(Application.dataPath, ConfigsSettings.ConfigsDirectoryName, configName);
        }
        
    }
}