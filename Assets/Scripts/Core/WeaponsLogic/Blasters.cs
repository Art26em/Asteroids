using Core.ObjectMovers;
using Core.ProjectilesLogic;
using Zenject;

namespace Core.WeaponsLogic
{
    public class Blasters
    {
        private BulletsProvider _bulletsProvider;
        private BulletMover _bulletMover;

        [Inject]
        private void Construct(BulletsProvider bulletsProvider, BulletMover bulletMover)
        {
            _bulletsProvider = bulletsProvider;
            _bulletMover = bulletMover;
        }
        
        public void Shoot()
        {
            var bullets = _bulletsProvider.GetBullets();
            foreach (var bullet in bullets)
            {
                _bulletMover.StartBulletMoving(bullet);
            }
        }
    }
}