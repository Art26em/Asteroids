using UnityEngine;

namespace Core.Factories
{
    public class AsteroidFactory
    {
        private readonly GameObject[] _largeAsteroidPrefabs;
        private GameObject[] _mediumAsteroidPrefabs;
        private GameObject[] _smallAsteroidPrefabs;
        private readonly GameObject _container;
        
        public AsteroidFactory(
            GameObject[] largeAsteroidPrefabs, 
            GameObject[] mediumAsteroidPrefabs,
            GameObject[] smallAsteroidPrefabs,
            GameObject container)
        {
            _largeAsteroidPrefabs = largeAsteroidPrefabs;
            _mediumAsteroidPrefabs = mediumAsteroidPrefabs;
            _smallAsteroidPrefabs = smallAsteroidPrefabs;
            _container = container;
        }
        
        public GameObject Create()
        {
            var prefab = _largeAsteroidPrefabs[Random.Range(0, _largeAsteroidPrefabs.Length)];
            return Object.Instantiate(prefab, _container.transform);
        }

        public void DestroyAsteroid(GameObject asteroid)
        {
            Object.Destroy(asteroid);
        }
        
    }
}