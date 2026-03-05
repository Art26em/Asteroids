using Core.AsteroidsLogic;
using Core.AsteroidsPresentation;
using Core.Configs;
using Core.ObjectFactories;
using Core.ObjectMovers;
using Core.ObjectPools;
using Core.ObjectSpawners;
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
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        { 
            Container.Bind<LargeAsteroid>().FromInstance(largeAsteroidPrefab).AsSingle();
            Container.Bind<ObjectPool<LargeAsteroid>>().AsSingle();
            Container.Bind<ObjectFactory<LargeAsteroid>>().AsSingle();
            Container.Bind<ObjectSpawner<LargeAsteroid>>().AsSingle();
            Container.Bind<LargeAsteroidMover>().AsSingle();
            
            Container.Bind<MediumAsteroid>().FromInstance(mediumAsteroidPrefab).AsSingle();
            Container.Bind<ObjectPool<MediumAsteroid>>().AsSingle();
            Container.Bind<ObjectFactory<MediumAsteroid>>().AsSingle();
            Container.Bind<ObjectSpawner<MediumAsteroid>>().AsSingle();
            Container.Bind<MediumAsteroidMover>().AsSingle();
            
            Container.Bind<AsteroidsController>().AsSingle();
            Container.Bind<WorldBoundsChecker>().AsSingle();
        }
    }
}