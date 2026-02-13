using System.Threading;
using Core.Entities.Player.Fighting.Projectiles;
using Core.ObjectPools;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Entities.Player.Fighting.Weapons
{
    public class Blasters
    {
        private readonly ObjectPool<Bullet> _magazine;
        private readonly Transform _shootPoint;
        private CancellationTokenSource _cancellationTokenSource;

        public Blasters(Bullet bulletPrefab, int magazineSize, Transform container, Transform shootPoint)
        {
            _shootPoint = shootPoint;
            _magazine = new ObjectPool<Bullet>();
            
            for (int i = 0; i < magazineSize; i++)
            {
                var bullet = Object.Instantiate(bulletPrefab, container);
                bullet.gameObject.SetActive(false);
                _magazine.Add(bullet);
            }
        }

        public void Shoot()
        {
            if (!_magazine.TryGetItem(out var bullet)) return;
            bullet.gameObject.SetActive(true);
            bullet.transform.position = _shootPoint.position;
            _ = MoveBullets(bullet);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private async UniTask MoveBullets(Bullet bullet)
        {
            while (bullet.isActiveAndEnabled)
            {
                bullet.transform.Translate(_shootPoint.up * (bullet.Speed * Time.deltaTime),Space.World);
                await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
            }
        }
        
    }
}