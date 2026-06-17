using Core.ObjectMovers;
using Core.PlayerLogic;
using Core.PlayerPresentation;
using Core.ScoreSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<PlayerInputData>().AsSingle();
            
            // if (Application.isMobilePlatform)
            // {
                 Container.Bind<IPlayerInputProvider>().To<PlayerMobileInputProvider>().AsSingle();    
            // }
            // else
            // {
            //     Container.Bind<IPlayerInputProvider>().To<PlayerKeyboardInputController>().AsSingle();    
            // }
            
            Container.Bind<Score>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle();
            Container.Bind<PlayerMover>().AsSingle();
            Container.Bind<PlayerDamageHandler>().AsSingle();
            Container.Bind<PlayerInvulnerabilityController>().AsSingle();
        }
    }
}