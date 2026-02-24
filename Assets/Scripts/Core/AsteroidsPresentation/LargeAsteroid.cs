using Core.ProjectilesPresentation;
using UnityEngine;

namespace Core.AsteroidsPresentation
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