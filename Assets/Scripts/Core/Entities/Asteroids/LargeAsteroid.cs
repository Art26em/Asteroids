using Core.Entities.Player.Fighting.Projectiles;
using UnityEngine;

namespace Core.Entities.Asteroids
{
    public class LargeAsteroid : Asteroid
    {
        protected override void HandleCollision(GameObject other)
        {
            if (other.TryGetComponent(out Bullet asteroid))
            {
                gameObject.SetActive(false);    
            }
        }
    }
}