using Core.AnimationsControllers;
using Core.StateControllers;
using Signals;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Animations settings")]
        [SerializeField] private Sprite playerIdleSprite;
        [SerializeField] private Sprite playerMovingSprite;
        [SerializeField] private Sprite playerRollLeftSprite;
        [SerializeField] private Sprite playerRollRightSprite;
        [SerializeField] private Transform earth;
        [SerializeField] private float earthMoveOutSpeed;
        [SerializeField] private Vector3 earthStartPosition;
        [SerializeField] private Vector3 earthTargetPosition;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            InstallSignals();
            InstallControllers();
        }
        
        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameStateChangedSignal>();
        }

        private void InstallControllers()
        {
            var earthAnimationSettings = new EarthAnimationSettings(
                earth,
                earthMoveOutSpeed,
                earthStartPosition,
                earthTargetPosition);

            var playerAnimationSettings = new PlayerAnimationSettings(
                playerIdleSprite,
                playerMovingSprite,
                playerRollLeftSprite,
                playerRollRightSprite);
            
            var animationController = new AnimationsController(earthAnimationSettings, playerAnimationSettings);    
            
            Container.Bind<AnimationsController>().FromInstance(animationController).AsSingle();
            Container.Bind<GameStartController>().FromNew().AsSingle().WithArguments(animationController);
            Container.Bind<GameOverController>().FromNew().AsSingle().WithArguments(animationController);
            
        }
        
    }
}