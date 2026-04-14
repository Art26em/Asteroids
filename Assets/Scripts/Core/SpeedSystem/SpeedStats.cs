using Newtonsoft.Json;
using UnityEngine;

namespace Core.SpeedSystem
{
    public class SpeedStats
    {
        [JsonIgnore]
        public Vector2 CurrentVelocity = new(0, 0);
        [JsonIgnore]
        public Vector2 CurrentDirection => CurrentVelocity.normalized;
        public float CurrentSpeed => CurrentVelocity.magnitude;
        public float MaxSpeed;
        public float Acceleration;
        public float Deceleration;
        public float RotationSpeed;
    }
}