using Core.ObjectPools;
using UnityEngine;
using Zenject;

namespace Core.Factories
{
    public class ObjectFactory<T> where T : Component
    {
        private T _objectPrefab;
        private readonly ObjectPool<T> _objectPool;
        private Transform _objectContainer;
        private int objectCount;
        
        public  ObjectFactory(
            T prefab, 
            ObjectPool<T> pool, 
            Transform container, 
            int objectCount, 
            DiContainer diContainer)
        {
            _objectPool = pool;
            for (int i = 0; i < objectCount; i++)
            {
                var item = diContainer.InstantiatePrefab(prefab, container).GetComponent<T>();
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