using System;
using Core.Configs;

namespace Core.Entities
{
    [Serializable]
    public class PlayerStats
    {
        private Health _health;
        private Speed _speed;
        
        private PlayerConfigLoader _playerConfigLoader;
        
        public PlayerStats(PlayerConfigLoader playerConfigLoader)
        {
            _playerConfigLoader = playerConfigLoader;
            LoadConfigs();
        }

        private void LoadConfigs()
        {
            var playerData = _playerConfigLoader.LoadConfigs();
            _health = new Health(playerData.MaxHealth);
            _speed = new Speed(playerData.MaxSpeed);
        }
    }
}