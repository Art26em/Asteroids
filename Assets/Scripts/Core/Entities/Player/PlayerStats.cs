using System;
using Core.Configs;
using Core.Entities.Health;
using Core.Entities.Speed;

namespace Core.Entities.Player
{
    [Serializable]
    public class PlayerStats
    {
        public HealthStats HealthStats;
        public SpeedStats SpeedStats;
        
        private ConfigManager<PlayerData> _configManager;
        
        public PlayerStats(ConfigManager<PlayerData> configManager)
        {
            _configManager = configManager;
            var playerData = configManager.LoadConfigs(ConfigsSettings.PlayerConfigName);
            HealthStats = playerData.HealthStats;
            SpeedStats = playerData.SpeedStats;
        }
        
    }
}