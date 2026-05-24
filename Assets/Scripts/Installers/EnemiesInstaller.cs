using Core.EnemiesLogic;
using Core.EnemiesPresentation;
using Core.ObjectFactories;
using Core.ObjectMovers;
using Core.ObjectPools;
using Core.ObjectSpawners;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemiesInstaller : MonoInstaller
    {
        [SerializeField] private LightEnemy lightEnemyPrefab;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        { 
            Container.Bind<LightEnemy>().FromInstance(lightEnemyPrefab).AsSingle();
            Container.Bind<ObjectPool<LightEnemy>>().AsSingle();
            Container.Bind<ObjectFactory<LightEnemy>>().AsSingle();
            Container.Bind<ObjectSpawner<LightEnemy>>().AsSingle();
            Container.Bind<LightEnemyMover>().AsSingle();
            Container.Bind<EnemiesController>().AsSingle();
        }
    }
}