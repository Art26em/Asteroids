using Newtonsoft.Json;
using UnityEngine;

namespace Core.SpeedSystem
{
    public class SpeedStats
    {
        [JsonIgnore]
        public Vector2 CurrentVelocity = new(0, 0);
        public float CurrentSpeed;
        public float MaxSpeed;
        public float Acceleration;
        public float Deceleration;
        public float RotationSpeed;
        
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