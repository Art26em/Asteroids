using Newtonsoft.Json;
using UnityEngine;

namespace Core.Configs
{
    public class JsonConfigLoader : IConfigLoader
    {
        public T Load<T>() where T : new()
        {
            var config = Resources.Load<TextAsset>(ConfigsSettings.GetConfigName<T>());
            return config == null ? new T() : JsonConvert.DeserializeObject<T>(config.text);
        }
    }
}