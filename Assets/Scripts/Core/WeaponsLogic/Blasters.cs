using Core.Configs;
using Core.ObjectMovers;
using Core.ProjectilesLogic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.WeaponsLogic
{
    public class Blasters
    {
        private BulletsProvider _bulletsProvider;
        private BulletMover _bulletMover;
        private float _reloadTime;
        private bool _canShoot = true;

        [Inject]
        private void Construct(
            BulletsProvider bulletsProvider, 
            BulletMover bulletMover, 
            ProjectilesData projectilesData)
        {
            _bulletsProvider = bulletsProvider;
            _bulletMover = bulletMover;
            _reloadTime = projectilesData.BlastersReloadTime;
        }
        
        public void Shoot()
        {
            if (!_canShoot) return;
            var bullets = _bulletsProvider.GetBullets();
            foreach (var bullet in bullets)
            {
                _bulletMover.StartBulletMoving(bullet);
            }
            _ = Reload();
        }
        
        private async UniTask Reload()
        {
            _canShoot = false;
            var elapsedTime = 0f;
            while (elapsedTime < _reloadTime)
            {
                elapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }
            _canShoot = true;
        }
        
    }
}