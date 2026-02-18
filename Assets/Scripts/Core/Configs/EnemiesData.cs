using Core.Entities.Health;
using Core.Entities.Speed;

namespace Core.Configs
{
    public class EnemiesData
    {
        public readonly HealthStats HealthStats = new();
        public readonly SpeedStats SpeedStats = new();
    }
}