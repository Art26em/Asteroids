using Core.ObjectFactories;
using UnityEngine;
using Zenject;

namespace Core.ObjectSpawners
{
    public class ObjectSpawner<T> where T : Component
    {
        private ObjectFactory<T> _factory;

        [Inject]
        private void Construct(ObjectFactory<T> factory)
        {
            _factory = factory;
        }
        
        public bool IsSpawnIntervalElapsed(float elapsedTime, float spawnTime)
        {
            return !(elapsedTime < spawnTime);
        }

        public bool TrySpawnObject(Transform spawnPoint, out T spawnedObject)
        {
            if (_factory.TryCreateObject(out var item))
            {
                spawnedObject = item;
                spawnedObject.gameObject.SetActive(true);
                spawnedObject.transform.position = spawnPoint.position;
                spawnedObject.transform.rotation = spawnPoint.rotation;
                return true;
            }
            spawnedObject = null;
            return false;
        }
        
    }
}