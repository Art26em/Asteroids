// using Core.Analytics;
using Core.Configs;
using Core.ObjectPools;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ConfigsInstaller : MonoInstaller
    {
        [Header("Asteroids")]
        [SerializeField] private Transform asteroidsContainer;
        [SerializeField] private Transform[] asteroidSpawnPositions;
        [Header("Projectiles")]
        [SerializeField] private Transform bulletsContainer;
        [SerializeField] private Transform[] shootPoints;
        [Header("Enemies")]
        [SerializeField] private Transform enemiesContainer;
        [SerializeField] private Transform[] enemiesSpawnPositions;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            IConfigLoader configRepository = new JsonConfigLoader();
            
            var asteroidsData = configRepository.Load<AsteroidsData>();
            asteroidsData.SetContainer(asteroidsContainer);
            asteroidsData.SetSpawnPositions(asteroidSpawnPositions);
            
            var projectilesData = configRepository.Load<ProjectilesData>();
            projectilesData.SetContainer(bulletsContainer);
            projectilesData.SetSpawnPositions(shootPoints);
            
            var enemiesData = configRepository.Load<EnemiesData>();
            enemiesData.SetContainer(enemiesContainer);
            enemiesData.SetSpawnPositions(enemiesSpawnPositions);
            
            var playerData = configRepository.Load<PlayerData>();
            
            Container.Bind<AsteroidsData>().FromInstance(asteroidsData).AsSingle();
            Container.Bind<PlayerData>().FromInstance(playerData).AsSingle();
            Container.Bind<ProjectilesData>().FromInstance(projectilesData).AsSingle();
            Container.Bind<EnemiesData>().FromInstance(enemiesData).AsSingle();
            Container.Bind<ObjectPoolSettings>().AsSingle();
            // Container.Bind<AnalyticsEventSender>().AsSingle();
        }
    }
}