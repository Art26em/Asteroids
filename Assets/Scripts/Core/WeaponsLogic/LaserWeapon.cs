using System;
using Core.Configs;
using Core.ProjectilesPresentation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.WeaponsLogic
{
    public class LaserWeapon
    {
        public event Action<float> LaserReloading;
        
        private Laser _laser;
        private float _laserFireTime;
        private float _laserReloadTime;
        private bool _canFire = true;

        [Inject]
        private void Construct(Laser laser, ProjectilesData projectilesData)
        {
            _laser = laser;
            _laserFireTime = projectilesData.LaserFireTime;
            _laserReloadTime = projectilesData.LaserReloadTime;
        }
        
        public void Shoot()
        {
            if (_canFire)
            {
                _ = FireLaser();
            }        
        }

        private async UniTask FireLaser()
        {
            _canFire = false;
            
            _laser.gameObject.SetActive(true);
            var elapsedTime = 0f;
            while (elapsedTime < _laserFireTime)
            {
                elapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }
            _laser.gameObject.SetActive(false);

            _ = ReloadLaser();
        }

        private async UniTask ReloadLaser()
        {
            var elapsedTime = 0f;
            while (elapsedTime < _laserReloadTime)
            {
                elapsedTime += Time.deltaTime;
                var remainingTime = Math.Max(0, _laserReloadTime - elapsedTime);
                LaserReloading?.Invoke(remainingTime);
                await UniTask.Yield();
            }
            _canFire = true;
        }
        
    }
}