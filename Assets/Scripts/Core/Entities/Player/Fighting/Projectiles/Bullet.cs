using Core.Configs;
using Core.Entities.Asteroids;
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
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (TryGetComponent(out Asteroid asteroid))
            {
                    
            }
        }
    }
}