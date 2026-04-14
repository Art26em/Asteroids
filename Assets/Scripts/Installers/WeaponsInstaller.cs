using Core.ObjectFactories;
using Core.ObjectMovers;
using Core.ObjectPools;
using Core.ObjectSpawners;
using Core.ProjectilesLogic;
using Core.ProjectilesPresentation;
using Core.WeaponsLogic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class WeaponsInstaller : MonoInstaller
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Laser laserPrefab;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<Bullet>().FromInstance(bulletPrefab).AsSingle();
            Container.Bind<ObjectPool<Bullet>>().AsSingle();
            Container.Bind<ObjectFactory<Bullet>>().AsSingle();
            Container.Bind<ObjectSpawner<Bullet>>().AsSingle();
            Container.Bind<BulletMover>().AsSingle();
            Container.Bind<BulletsProvider>().AsSingle();
            Container.Bind<Blasters>().AsSingle();
            
            Container.Bind<Laser>().FromInstance(laserPrefab).AsSingle();
            Container.Bind<LaserWeapon>().AsSingle();
        }
    }
}