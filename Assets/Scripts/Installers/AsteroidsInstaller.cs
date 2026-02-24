using Core.AsteroidsPresentation;
using Core.Configs;
using Core.Factories;
using Core.ObjectMovers;
using Core.ObjectPools;
using Core.Spawners;
using Core.World;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class AsteroidsInstaller : MonoInstaller
    {
        [SerializeField] private LargeAsteroid largeAsteroidPrefab;
        [SerializeField] private MediumAsteroid mediumAsteroidPrefab;
        [SerializeField] private SmallAsteroid smallAsteroidPrefab;
        [SerializeField] private Transform asteroidsContainer;
        [SerializeField] private Transform[] asteroidSpawnPositions;
    
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            var asteroidsConfigManager = new ConfigManager<AsteroidsData>();
            var asteroidsData = asteroidsConfigManager.LoadConfigs(ConfigsSettings.AsteroidsConfigName);
        
            Container.Bind<AsteroidsData>().FromInstance(asteroidsData).AsSingle();
        
             Container.Bind<LargeAsteroid>().FromInstance(largeAsteroidPrefab).AsSingle();
             Container.Bind<ObjectPool<LargeAsteroid>>().AsSingle();
             Container.Bind<ObjectFactory<LargeAsteroid>>().AsSingle().WithArguments(
                 asteroidsContainer, 
                 asteroidsData.LargeAsteroidPoolSize);
             Container.Bind<ObjectSpawner<LargeAsteroid>>().AsSingle().WithArguments(
                 asteroidSpawnPositions,
                 asteroidsData.TimeToSpawn);
             Container.Bind<IMover<LargeAsteroid>>().To<LargeAsteroidMover>().AsSingle();
            
            Container.Bind<WorldBoundsChecker>().AsSingle();
        }
    }
}