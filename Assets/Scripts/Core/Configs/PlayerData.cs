using Core.Entities.Health;
using Core.Entities.Speed;

namespace Core.Configs
{
    public class PlayerData
    {
        public readonly HealthStats HealthStats = new();
        public readonly SpeedStats SpeedStats = new();
    }
}