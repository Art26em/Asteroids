using Core.Entities.Asteroids;
using Core.ObjectPools;
using UnityEngine;

namespace Core.Factories
{
    public class AsteroidFactory
    {
        private readonly ObjectPool<LargeAsteroid> _largeAsteroidsPool;
        private readonly ObjectPool<MediumAsteroid> _mediumAsteroidsPool;
        private readonly ObjectPool<SmallAsteroid> _smallAsteroidsPool;
        
        public AsteroidFactory(
            LargeAsteroid largeAsteroidPrefab, 
            MediumAsteroid mediumAsteroidPrefab, 
            SmallAsteroid smallAsteroidPrefab, 
            GameObject container,
            int asteroidsCount)
        {
            _largeAsteroidsPool = new ObjectPool<LargeAsteroid>();
            _mediumAsteroidsPool = new ObjectPool<MediumAsteroid>();
            _smallAsteroidsPool = new ObjectPool<SmallAsteroid>();

            for (var i = 0; i < asteroidsCount; i++)
            {
                var newLargeAsteroid = Object.Instantiate(largeAsteroidPrefab, container.transform);
                newLargeAsteroid.gameObject.SetActive(false);
                _largeAsteroidsPool.Add(newLargeAsteroid);
                
                var newMediumAsteroid = Object.Instantiate(mediumAsteroidPrefab, container.transform);
                newMediumAsteroid.gameObject.SetActive(false);
                _mediumAsteroidsPool.Add(newMediumAsteroid);
                
                var newSmallAsteroid = Object.Instantiate(smallAsteroidPrefab, container.transform);
                newSmallAsteroid.gameObject.SetActive(false);
                _smallAsteroidsPool.Add(newSmallAsteroid);
            }
            
        }
        
        public bool TryCreateLargeAsteroid(out LargeAsteroid largeAsteroid)
        {
            return _largeAsteroidsPool.TryGetItem(out largeAsteroid);
        }

        public bool TryCreateMediumAsteroid(out MediumAsteroid mediumAsteroid)
        {
            return _mediumAsteroidsPool.TryGetItem(out mediumAsteroid);
        }
        
        public bool TryCreateSmallAsteroid(out SmallAsteroid smallAsteroid)
        {
            return _smallAsteroidsPool.TryGetItem(out smallAsteroid);
        }
        
        public void DestroyLargeAsteroid(LargeAsteroid asteroid)
        {
            _largeAsteroidsPool.ReturnItemToPool(asteroid);
        }
        
        public void DestroyMediumAsteroid(MediumAsteroid asteroid)
        {
            _mediumAsteroidsPool.ReturnItemToPool(asteroid);
        }
        
        public void DestroySmallAsteroid(SmallAsteroid asteroid)
        {
            _smallAsteroidsPool.ReturnItemToPool(asteroid);
        }
        
    }
}