using Core.Configs;
using Core.Factories;
using Core.ObjectMovers;
using Core.ObjectPools;
using Core.ProjectilesPresentation;
using Core.Spawners;
using Core.WeaponsLogic;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class WeaponsInstaller : MonoInstaller
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform bulletsContainer;
        [SerializeField] private Transform[] shootPoints;
    
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            var projectilesConfigManager = new ConfigManager<ProjectilesData>();
            var projectilesData = projectilesConfigManager.LoadConfigs(ConfigsSettings.ProjectilesConfigName);
            
            Container.Bind<ProjectilesData>().FromInstance(projectilesData).AsSingle();
            Container.Bind<Bullet>().FromInstance(bulletPrefab).AsSingle();
            Container.Bind<ObjectPool<Bullet>>().AsSingle();
            Container.Bind<ObjectFactory<Bullet>>().AsSingle().WithArguments(bulletsContainer, projectilesData.MagazineSize);
            Container.Bind<ObjectSpawner<Bullet>>().AsSingle().WithArguments(shootPoints);
            Container.Bind<IMover<Bullet>>().To<BulletMover>().AsSingle();
            Container.BindInterfacesAndSelfTo<Blasters>().AsSingle();
        }
    }
}