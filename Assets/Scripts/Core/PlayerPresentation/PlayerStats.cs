using System;
using Core.Configs;
using Core.HealthSystem;
using Core.SpeedSystem;
using Zenject;

namespace Core.PlayerPresentation
{
    [Serializable]
    public class PlayerStats
    {
        public HealthStats HealthStats;
        public SpeedStats SpeedStats;

        [Inject]
        private void Construct(PlayerData playerData)
        {
            HealthStats = playerData.HealthStats;
            SpeedStats = playerData.SpeedStats;    
        }
        
    }
}