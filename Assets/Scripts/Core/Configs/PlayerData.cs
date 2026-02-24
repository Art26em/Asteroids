using Core.HealthSystem;
using Core.SpeedSystem;

namespace Core.Configs
{
    public class PlayerData
    {
        public readonly HealthStats HealthStats = new();
        public readonly SpeedStats SpeedStats = new();
    }
}