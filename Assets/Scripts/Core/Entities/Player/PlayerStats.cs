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
        
        public PlayerStats(PlayerData playerData)
        {
            HealthStats = playerData.HealthStats;
            SpeedStats = playerData.SpeedStats;
        }
        
    }
}