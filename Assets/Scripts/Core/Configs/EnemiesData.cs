using Core.HealthSystem;
using Core.SpeedSystem;

namespace Core.Configs
{
    public class EnemiesData
    {
        public readonly HealthStats HealthStats;
        public readonly SpeedStats SpeedStats;

        public EnemiesData(object enemyObject)
        {
            if (enemyObject is not EnemiesData enemiesData) return;
            HealthStats = enemiesData.HealthStats;
            SpeedStats = enemiesData.SpeedStats;
        }
    }
}