using UnityEngine;

namespace Signals
{
    public struct LargeAsteroidDestroyedSignal
    {
        public readonly Transform AsteroidTransform;

        public LargeAsteroidDestroyedSignal(Transform asteroidTransform)
        {
            AsteroidTransform = asteroidTransform;
        }
        
    }
}