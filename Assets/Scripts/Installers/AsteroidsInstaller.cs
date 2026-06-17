using Core.AsteroidsLogic;
using Core.AsteroidsPresentation;
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
        [SerializeField] private LargeAsteroid _largeAsteroidPrefab;
        [SerializeField] private MediumAsteroid _mediumAsteroidPrefab;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        { 
            Container.Bind<LargeAsteroid>().FromInstance(_largeAsteroidPrefab).AsSingle();
            Container.Bind<ObjectPool<LargeAsteroid>>().AsSingle();
            Container.Bind<ObjectFactory<LargeAsteroid>>().AsSingle();
            Container.Bind<ObjectSpawner<LargeAsteroid>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LargeAsteroidMover>().AsSingle();
            
            Container.Bind<MediumAsteroid>().FromInstance(_mediumAsteroidPrefab).AsSingle();
            Container.Bind<ObjectPool<MediumAsteroid>>().AsSingle();
            Container.Bind<ObjectFactory<MediumAsteroid>>().AsSingle();
            Container.Bind<ObjectSpawner<MediumAsteroid>>().AsSingle();
            Container.Bind<MediumAsteroidMover>().AsSingle();
            
            Container.Bind<AsteroidsController>().AsSingle();
            Container.Bind<WorldBoundsChecker>().AsSingle();
        }
    }
}