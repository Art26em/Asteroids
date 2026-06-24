using System;
using Core.Configs;
using Core.HealthSystem;
using Core.SpeedSystem;
using UnityEngine;
using Zenject;

namespace Core.PlayerPresentation
{
    public class PlayerStats : IInitializable, IDisposable
    {
        public HealthStats HealthStats;
        public int CurrentHealth => HealthStats.CurrentHealth;
        public SpeedStats SpeedStats;
        public bool IsImmortal;
        public float InvincibilityTime;
        
        public event Action<int> HealthChanged;
        public event Action<float> VelocityChanged;
        public event Action<Vector2> PositionChanged;
        public event Action<Vector3> RotationChanged;
        
        private Vector2 _currentPosition;
        public Vector2 CurrentPosition
        {
            get => _currentPosition;
            set
            {
                _currentPosition = value;
                PositionChanged?.Invoke(CurrentPosition);    
            }
        }
        
        [Inject]
        private void Construct(PlayerData playerData)
        {
            InvincibilityTime = playerData.InvincibilityTime;
            HealthStats = new HealthStats
            {
                MaxHealth = playerData.HealthStats.MaxHealth,
                CurrentHealth = playerData.HealthStats.CurrentHealth
            };
            SpeedStats = new SpeedStats
            {
                MaxSpeed = playerData.SpeedStats.MaxSpeed,
                Acceleration = playerData.SpeedStats.Acceleration,
                Deceleration = playerData.SpeedStats.Deceleration,
                RotationSpeed = playerData.SpeedStats.RotationSpeed
            };
        }
        
        public void Initialize()
        {
            HealthStats.HealthChanged += OnHealthChanged;
            SpeedStats.VelocityChanged += OnVelocityChanged;
            SpeedStats.RotationChanged += OnRotationChanged;
        }

        public void Dispose()
        {
            HealthStats.HealthChanged -= OnHealthChanged;
            SpeedStats.VelocityChanged -= OnVelocityChanged;
            SpeedStats.RotationChanged -= OnRotationChanged;
        }
        
        private void OnHealthChanged()
        {
            HealthChanged?.Invoke(HealthStats.CurrentHealth);
        }

        private void OnVelocityChanged()
        {
            VelocityChanged?.Invoke(SpeedStats.CurrentSpeed);
        }

        private void OnRotationChanged()
        {
            RotationChanged?.Invoke(SpeedStats.CurrentRotation);
        }
        
    }
}