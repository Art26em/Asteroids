using System;
using System.Collections.Generic;
using Core.AsteroidsPresentation;
using Core.Configs;
using Core.EnemiesPresentation;
using Core.ProjectilesPresentation;
using Zenject;

namespace Core.ObjectPools
{
    public class ObjectPoolSettings : IPoolSettings
    {
        private AsteroidsData _asteroidsData;
        private ProjectilesData _projectilesData;
        private EnemiesData _enemiesData;
        private Dictionary<Type, IPoolSettings.Settings> _settings;

        [Inject]
        private void Construct(AsteroidsData asteroidsData, ProjectilesData projectilesData, EnemiesData enemiesData)
        {
            _asteroidsData = asteroidsData;
            _projectilesData = projectilesData;
            _enemiesData = enemiesData;
            
            CreateSettings();
        }
        
        private void CreateSettings()
        {
            _settings = new Dictionary<Type, IPoolSettings.Settings>();

            var largeAsteroidSettings = new IPoolSettings.Settings
            {
                Container = _asteroidsData.AsteroidsContainer,
                Count = _asteroidsData.LargeAsteroidPoolSize
            };
            
            var mediumAsteroidSettings = new IPoolSettings.Settings
            {
                Container = _asteroidsData.AsteroidsContainer,
                Count = _asteroidsData.MediumAsteroidCount
            };
            
            var projectileSettings = new IPoolSettings.Settings
            {
                Count = _projectilesData.MagazineSize,
                Container = _projectilesData.BulletsContainer  
            };
            
            var lightEnemySettings = new IPoolSettings.Settings
            {
                Count = _enemiesData.LightEnemyPoolSize,
                Container = _enemiesData.EnemiesContainer 
            };
            
            _settings.Add(typeof(LargeAsteroid), largeAsteroidSettings);
            _settings.Add(typeof(MediumAsteroid), mediumAsteroidSettings);
            _settings.Add(typeof(Bullet), projectileSettings);
            _settings.Add(typeof(LightEnemy), lightEnemySettings);
        }
        
        public IPoolSettings.Settings GetSettings<T>()
        {
            return !_settings.TryGetValue(typeof(T), out var settings) ? 
                throw new KeyNotFoundException($"Key {typeof(T).Name} does not exist in settings pool") : settings;
        }
    }
}