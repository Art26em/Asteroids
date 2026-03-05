using Core.ProjectilesPresentation;
using Signals;
using UnityEngine;

namespace Core.AsteroidsPresentation
{
    public class LargeAsteroid : Asteroid
    {
        protected override void HandleCollision(GameObject other)
        {
            if (other.TryGetComponent(out Bullet _))
            {
                SignalBus.Fire(new LargeAsteroidDestroyedSignal(gameObject.transform));
                gameObject.SetActive(false);  
            }
        }
    }
}