using System;
using Core.Configs;
using Core.Entities.Speed;

namespace Core.Entities.Player
{
    [Serializable]
    public class PlayerStats
    {
        public HealthStats HealthStats;
        public SpeedStats SpeedStats;
        
        private PlayerConfigLoader _playerConfigLoader;
        
        public PlayerStats(PlayerConfigLoader playerConfigLoader)
        {
            _playerConfigLoader = playerConfigLoader;
            LoadConfigs();
        }

        private void LoadConfigs()
        {
            var playerData = _playerConfigLoader.LoadConfigs();
            HealthStats = new HealthStats(playerData.MaxHealth);
            SpeedStats = new SpeedStats(playerData.MaxSpeed);
        }
    }
}