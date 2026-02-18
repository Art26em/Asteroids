using Core.Entities.Player.Fighting.Projectiles;
using Core.Spawners;
using UnityEngine;

namespace Core.Entities.Asteroids
{
    public class LargeAsteroid : Asteroid
    {
        private ObjectSpawner<MediumAsteroid> _objectSpawner;
        
        protected override void HandleCollision(GameObject other)
        {
            if (other.TryGetComponent(out Bullet asteroid))
            {
                gameObject.SetActive(false);    
            }
        }
    }
}