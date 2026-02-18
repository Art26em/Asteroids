using Core.Configs;
using Core.Entities.Asteroids;
using Core.Entities.Asteroids.Movement;
using Core.Factories;
using Core.ObjectMovers;
using Core.ObjectPools;
using Core.Spawners;
using Core.World;
using UnityEngine;
using Zenject;

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
        
        Container.Bind<MediumAsteroid>().FromInstance(mediumAsteroidPrefab).AsSingle();
        Container.Bind<ObjectPool<MediumAsteroid>>().AsSingle();
        Container.Bind<ObjectFactory<MediumAsteroid>>().AsSingle().WithArguments(
            asteroidsContainer, 
            asteroidsData.LargeAsteroidPoolSize);
        Container.Bind<ObjectSpawner<MediumAsteroid>>().AsSingle().WithArguments(
            asteroidSpawnPositions,
            asteroidsData.TimeToSpawn);
        Container.Bind<IMover<MediumAsteroid>>().To<MediumAsteroidMover>().AsSingle();
        
        Container.Bind<WorldBoundsChecker>().AsSingle();
    }
}