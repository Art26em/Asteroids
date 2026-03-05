using Core.Configs;
using Core.PlayerPresentation;
using UnityEngine;
using Zenject;

namespace Core.ProjectilesPresentation
{
    public class Bullet : Projectile
    {
        public float Speed { get; private set; }
        
        [Inject]
        private void Construct(ProjectilesData projectilesData)
        {
            Speed = projectilesData.BulletSpeed;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerObject _)) return;
            DestroyBullet();
        }
        
        private void DestroyBullet()
        {
            gameObject.SetActive(false);
        }
    }
}