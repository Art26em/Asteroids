using Core.ProjectilesPresentation;
using UnityEngine;

namespace Core.AsteroidsPresentation
{
    public class MediumAsteroid : Asteroid
    {
        protected override void HandleCollision(GameObject other)
        {
            if (other.TryGetComponent(out Bullet _))
            {
                gameObject.SetActive(false);  
            }
        }
    }
}