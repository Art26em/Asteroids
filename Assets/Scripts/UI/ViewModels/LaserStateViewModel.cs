using System;
using Core.WeaponsLogic;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class LaserStateViewModel : IInitializable, IDisposable
    {
        [Data("LaserState")]
        public readonly ReactiveProperty<string> LaserStateView = new();
        private LaserWeapon _laserWeapon;

        [Inject]
        private void Construct(LaserWeapon laserWeapon)
        {
            _laserWeapon = laserWeapon;
        }

        public void Initialize()
        {
            OnLaserReloading();
            _laserWeapon.LaserReloading += OnLaserReloading;
        }

        public void Dispose()
        {
            _laserWeapon.LaserReloading -= OnLaserReloading;
        }

        private void OnLaserReloading(float remainingTime = 0)
        {
            LaserStateView.Value = remainingTime > 0 ? 
                "Laser: " + remainingTime.ToString("F2") : 
                "Laser: Ready";        
        }
        
    }
}