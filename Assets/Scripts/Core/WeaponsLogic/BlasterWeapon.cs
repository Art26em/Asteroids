using Core.Configs;
using Core.ObjectMovers;
using Core.ProjectilesLogic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.WeaponsLogic
{
    public class BlasterWeapon
    {
        private BulletsProvider _bulletsProvider;
        private BulletMover _bulletMover;
        private ProjectilesData _projectilesData;
        private bool _canShoot = true;

        [Inject]
        private void Construct(
            BulletsProvider bulletsProvider, 
            BulletMover bulletMover, 
            ProjectilesData projectilesData)
        {
            _bulletsProvider = bulletsProvider;
            _bulletMover = bulletMover;
            _projectilesData = projectilesData;
        }
        
        public void Shoot()
        {
            if (!_canShoot) return;
            
            foreach (var spawnPoint in _projectilesData.BulletsShootPoints)
            {
                if (_bulletsProvider.TryGetBullet(spawnPoint, out var bullet))
                {
                    _bulletMover.StartBulletMoving(bullet);
                }     
            }
            
            _ = Reload();
        }
        
        private async UniTask Reload()
        {
            _canShoot = false;
            var elapsedTime = 0f;
            while (elapsedTime < _projectilesData.BlastersReloadTime)
            {
                elapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }
            _canShoot = true;
        }
        
    }
}