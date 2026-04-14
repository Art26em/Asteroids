using Core.Configs;
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
            var asteroidsConfigManager = new ConfigManager<AsteroidsData>();
            var asteroidsData = asteroidsConfigManager.LoadConfigs(ConfigsSettings.AsteroidsConfigName);
            asteroidsData.SetContainer(asteroidsContainer);
            asteroidsData.SetSpawnPositions(asteroidSpawnPositions);
            
            var playerConfigManager = new ConfigManager<PlayerData>();
            var playerData = playerConfigManager.LoadConfigs(ConfigsSettings.PlayerConfigName);
            
            var projectilesConfigManager = new ConfigManager<ProjectilesData>();
            var projectilesData = projectilesConfigManager.LoadConfigs(ConfigsSettings.ProjectilesConfigName);
            projectilesData.SetContainer(bulletsContainer);
            projectilesData.SetSpawnPositions(shootPoints);
            
            var enemiesConfigManager = new ConfigManager<EnemiesData>();
            var enemiesData = enemiesConfigManager.LoadConfigs(ConfigsSettings.EnemiesConfigName);
            enemiesData.SetContainer(enemiesContainer);
            enemiesData.SetSpawnPositions(enemiesSpawnPositions);
            
            Container.Bind<AsteroidsData>().FromInstance(asteroidsData).AsSingle();
            Container.Bind<PlayerData>().FromInstance(playerData).AsSingle();
            Container.Bind<ProjectilesData>().FromInstance(projectilesData).AsSingle();
            Container.Bind<EnemiesData>().FromInstance(enemiesData).AsSingle();
        }
    }
}