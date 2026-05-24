using System;
using Core.PlayerPresentation;
using MVVM;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.ViewModels
{
    public class MovementStatsViewModel : IInitializable, IDisposable
    {
        [Data("Speed")]
        public readonly ReactiveProperty<string> SpeedView = new();
        [Data("Rotation")]
        public readonly ReactiveProperty<string> RotationView = new();
        [Data("PosX")]
        public readonly ReactiveProperty<string> PosXView = new();
        [Data("PosY")]
        public readonly ReactiveProperty<string> PosYView = new();
        
        private PlayerStats _playerStats;

        [Inject]
        private void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }
        
        public void Initialize()
        {
            _playerStats.VelocityChanged += OnVelocityChanged;
            _playerStats.PositionChanged += OnPositionChanged;
            _playerStats.RotationChanged += OnRotationChanged;
        }

        public void Dispose()
        {
            _playerStats.VelocityChanged -= OnVelocityChanged;
            _playerStats.PositionChanged -= OnPositionChanged;
            _playerStats.RotationChanged -= OnRotationChanged;
        }

        private void OnVelocityChanged(float speed)
        {
            SpeedView.Value = "Speed: " + speed.ToString("F2");
        }

        private void OnPositionChanged(Vector2 position)
        {
            PosXView.Value = "X: " + position.x.ToString("F2");
            PosYView.Value = "Y: " + position.y.ToString("F2");    
        }
        
        private void OnRotationChanged(Vector3 rotation)
        {
            RotationView.Value = "Rotation: " + rotation.z.ToString("F2");   
        }
        
    }
}