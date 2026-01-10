using System;
using Core.Configs;

namespace Core.Entities
{
    [Serializable]
    public class PlayerStats
    {
        public Health Health;
        public Speed Speed;
        
        private PlayerConfigLoader _playerConfigLoader;
        
        public PlayerStats(PlayerConfigLoader playerConfigLoader)
        {
            _playerConfigLoader = playerConfigLoader;
            LoadConfigs();
        }

        private void LoadConfigs()
        {
            var playerData = _playerConfigLoader.LoadConfigs();
            Health = new Health(playerData.MaxHealth);
            Speed = new Speed(playerData.MaxSpeed);
        }
    }
}