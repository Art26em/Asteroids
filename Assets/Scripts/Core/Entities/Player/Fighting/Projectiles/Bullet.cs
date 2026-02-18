using Core.Configs;
using UnityEngine;
using Zenject;

namespace Core.Entities.Player.Fighting.Projectiles
{
    public class Bullet : MonoBehaviour
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