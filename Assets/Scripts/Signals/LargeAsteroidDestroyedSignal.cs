using UnityEngine;

namespace Signals
{
    public struct LargeAsteroidDestroyedSignal
    {
        public Transform AsteroidTransform;

        public LargeAsteroidDestroyedSignal(Transform asteroidTransform)
        {
            AsteroidTransform = asteroidTransform;
        }
        
    }
}