using Core.Entities.Asteroids;
using Core.ObjectPools;
using UnityEngine;

namespace Core.Factories
{
    public class AsteroidFactory
    {
        private readonly GameObject[] _asteroidPrefabs;
        private readonly GameObject[] _asteroidPrefabs2;
        private readonly GameObject[] _asteroidPrefabs3;
        private readonly GameObject _container;
        
        private readonly ObjectPool<Asteroid> _asteroidPool1;
        private readonly ObjectPool<Asteroid> _asteroidPool2;
        private readonly ObjectPool<Asteroid> _asteroidPool3;
        
        private int _currentAsteroidPrefabIndex = 0;
        
        public AsteroidFactory(
            GameObject[] asteroidPrefabs, 
            GameObject[] asteroidPrefabs2,
            GameObject[] asteroidPrefabs3,
            GameObject container)
        {
            _asteroidPrefabs = asteroidPrefabs;
            _asteroidPrefabs2 = asteroidPrefabs2;
            _asteroidPrefabs3 = asteroidPrefabs3;
            _container = container;
            
            _asteroidPool1 = new ObjectPool<Asteroid>();
            _asteroidPool2 = new ObjectPool<Asteroid>();
            _asteroidPool3 = new ObjectPool<Asteroid>();
            
            
            
        }
        
        public GameObject Create()
        {
            var prefab = _asteroidPrefabs[_currentAsteroidPrefabIndex];
            return Object.Instantiate(prefab, _container.transform);
        }

        public void DestroyAsteroid(GameObject asteroid)
        {
            Object.Destroy(asteroid);
        }
        
    }
}