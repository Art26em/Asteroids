using System;
using Core.Configs;
using Core.HealthSystem;
using Core.SpeedSystem;

namespace Core.PlayerPresentation
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