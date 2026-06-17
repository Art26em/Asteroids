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
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Laser _laserPrefab;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<Bullet>().FromInstance(_bulletPrefab).AsSingle();
            Container.Bind<ObjectPool<Bullet>>().AsSingle();
            Container.Bind<ObjectFactory<Bullet>>().AsSingle();
            Container.Bind<ObjectSpawner<Bullet>>().AsSingle();
            Container.Bind<BulletMover>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletsProvider>().AsSingle();
            Container.Bind<BlasterWeapon>().AsSingle();
            
            Container.Bind<Laser>().FromInstance(_laserPrefab).AsSingle();
            Container.Bind<LaserWeapon>().AsSingle();
        }
    }
}