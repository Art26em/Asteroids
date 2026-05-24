using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.SpeedSystem
{
    public class SpeedStats
    {
        [JsonIgnore]
        private Vector2 _currentVelocity;
        private Vector3 _currentRotation;
        
        public Vector2 CurrentVelocity
        {
            get => _currentVelocity;
            set
            {
                _currentVelocity = value;
                VelocityChanged?.Invoke();
            }
        }
        public Vector3 CurrentRotation
        {
            get => _currentRotation;
            set
            {
                _currentRotation = value;
                RotationChanged?.Invoke();
            }
        }
        
        public event Action VelocityChanged;
        public event Action RotationChanged;
        
        [JsonIgnore]
        public Vector2 CurrentDirection => CurrentVelocity.normalized;
        public float CurrentSpeed => CurrentVelocity.magnitude;
        public float MaxSpeed;
        public float Acceleration;
        public float Deceleration;
        public float RotationSpeed;
    }
}