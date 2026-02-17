using Core.Configs;
using Core.Entities.Player.Fighting.Projectiles;
using Core.ObjectMovers;
using Core.ObjectPools;
using Core.World;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Entities.Player.Fighting.ProjectileMovers
{
    public class BulletMover : IMover<Bullet>
    {
        private readonly float _bulletSpeed;
        private readonly ObjectPool<Bullet> _magazine;
        private readonly WorldBoundsChecker _worldBoundsChecker;

        public BulletMover(
            ProjectilesData projectilesData, ObjectPool<Bullet> magazine, 
            WorldBoundsChecker worldBoundsChecker)
        {
            _bulletSpeed = projectilesData.BulletSpeed;
            _magazine = magazine;
            _worldBoundsChecker = worldBoundsChecker;
        }
        
        public void StartObjectMoving(Bullet item)
        {
            _ = MoveBullet(item);
        }
        
        private async UniTask MoveBullet(Bullet bullet)
        {
            while (bullet && bullet.isActiveAndEnabled && Application.isPlaying)
            {
                if (!bullet) break;
                bullet.transform.Translate(
                    bullet.transform.up * (_bulletSpeed * Time.deltaTime), 
                    Space.World);
                _worldBoundsChecker.ReturnObjectToPool(bullet.transform.position, bullet, _magazine);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }
        
    }
}