using Core.AsteroidsPresentation;
using Core.EnemiesPresentation;
using Core.ObjectPools;
using Core.ProjectilesPresentation;
using UnityEngine;
using Zenject;

namespace Core.ObjectFactories
{
    public class ObjectFactory<T> where T : Component
    {
        private ObjectPool<T> _objectPool;

        [Inject]
        private void Construct(T prefab, ObjectPool<T> pool, ObjectPoolSettings poolSettings, DiContainer diContainer)
        {
            _objectPool = pool;
            var settings = poolSettings.GetSettings<T>();

            for (var i = 0; i < settings.Count; i++)
            {
                var item = settings.Container ? 
                    diContainer.InstantiatePrefab(prefab, settings.Container).GetComponent<T>() : 
                    diContainer.InstantiatePrefab(prefab).GetComponent<T>();
                item.gameObject.SetActive(false);
                _objectPool.Add(item); 
            }    
        }
        
        public bool TryCreateObject(out T item)
        {
            return _objectPool.TryGetItem(out item);
        }
        
    }
}