
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Entities.Speed
{
    public class SpeedStats
    {
        [JsonIgnore]
        public Vector2 CurrentVelocity = new(0, 0);
        public float CurrentSpeed = 0;
        public float MaxSpeed { get; private set; } = 5;
        public float Acceleration { get; private set; } = 7;
        public float Deceleration { get; private set; } = 7;
        public float RotationSpeed { get; private set; } = 160;

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