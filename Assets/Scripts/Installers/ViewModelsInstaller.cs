using UI.ViewModels;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ViewModelsInstaller : MonoInstaller
    {
        [SerializeField] private Sprite healthSprite;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScoreViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MovementStatsViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LaserStateViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HealthBarViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HealthItemViewModel>().AsSingle().NonLazy();
        }
    }
}