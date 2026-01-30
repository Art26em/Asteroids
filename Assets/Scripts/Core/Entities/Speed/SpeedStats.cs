using UnityEngine;

namespace Core.Entities.Speed
{
    public class SpeedStats
    {
        public float CurrentSpeed = 0;
        public Vector2 CurrentVelocity;
        public float MaxSpeed { get; private set; } = 5;
        public float Acceleration { get; private set; } = 7;
        public float Deceleration { get; private set; } = 7;

        public SpeedStats()
        {
            CurrentVelocity = new Vector2(0, 0);
        }

        public void IncreaseCurrentSpeed(float amount)
        {
            CurrentSpeed += amount;
        }

        public void DecreaseCurrentSpeed(float amount)
        {
            CurrentSpeed -= amount;
        }
    }
}