using Core.AsteroidsPresentation;
using Core.Configs;
using Core.EnemiesPresentation;
using Core.ObjectFactories;
using Core.ProjectilesPresentation;
using Zenject;

namespace Core.ObjectPools
{
    public class ObjectPoolSettings : IPoolSettings
    {
        private AsteroidsData _asteroidsData;
        private ProjectilesData _projectilesData;
        private EnemiesData _enemiesData;

        [Inject]
        private void Construct(AsteroidsData asteroidsData, ProjectilesData projectilesData, EnemiesData enemiesData)
        {
            _asteroidsData = asteroidsData;
            _projectilesData = projectilesData;
            _enemiesData = enemiesData;
        }
        
        public IPoolSettings.Settings GetSettings<T>() where T : new()
        {
            var settings = new IPoolSettings.Settings();
            
            var typeT = typeof(T);
            
            if (typeT.IsSubclassOf(typeof(Asteroid)))
            {
                settings.Count = typeT == typeof(LargeAsteroid) ? 
                    _asteroidsData.LargeAsteroidPoolSize :
                    _asteroidsData.MediumAsteroidPoolSize;
                settings.Container = _asteroidsData.AsteroidsContainer;    
            } else if (typeT.IsSubclassOf(typeof(Projectile)))
            {
                settings.Count = _projectilesData.MagazineSize;
                settings.Container = _projectilesData.BulletsContainer;    
            }
            else if(typeT.IsSubclassOf(typeof(Enemy)))
            {
                settings.Count = typeT == typeof(LightEnemy) ? 
                    _enemiesData.LightEnemyPoolSize :
                    _enemiesData.MediumEnemyPoolSize;
                settings.Container = _enemiesData.EnemiesContainer;        
            }
            
            return settings;
        }
    }
}