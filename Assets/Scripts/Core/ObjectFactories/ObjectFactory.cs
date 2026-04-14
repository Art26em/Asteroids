using Core.AsteroidsPresentation;
using Core.Configs;
using Core.EnemiesPresentation;
using Core.ObjectPools;
using Core.ProjectilesPresentation;
using UnityEngine;
using Zenject;

namespace Core.ObjectFactories
{
    public class ObjectFactory<T> where T : Component
    {
        private T _objectPrefab;
        private ObjectPool<T> _objectPool;
        private Transform _objectContainer;

        [Inject]
        private void Construct(T prefab, ObjectPool<T> pool, DiContainer diContainer)
        {
            _objectPool = pool;
            var objectCount = 0;
            Transform container = null;
            var typeT = typeof(T);
            
            if (typeT.IsSubclassOf(typeof(Asteroid)))
            {
                var asteroidsData = diContainer.Resolve<AsteroidsData>();
                objectCount = typeT == typeof(LargeAsteroid) ? 
                    asteroidsData.LargeAsteroidPoolSize :
                    asteroidsData.MediumAsteroidPoolSize;
                container = asteroidsData.AsteroidsContainer;    
            } else if (typeT.IsSubclassOf(typeof(Projectile)))
            {
                objectCount = diContainer.Resolve<ProjectilesData>().MagazineSize;
                container = diContainer.Resolve<ProjectilesData>().BulletsContainer;    
            }
            else if(typeT.IsSubclassOf(typeof(Enemy)))
            {
                var enemiesData = diContainer.Resolve<EnemiesData>();
                objectCount = typeT == typeof(LightEnemy) ? 
                    enemiesData.LightEnemyPoolSize :
                    enemiesData.MediumEnemyPoolSize;
                container = enemiesData.EnemiesContainer;        
            }
            
            for (var i = 0; i < objectCount; i++)
            {
                var item = container ? 
                    diContainer.InstantiatePrefab(prefab, container).GetComponent<T>() : 
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