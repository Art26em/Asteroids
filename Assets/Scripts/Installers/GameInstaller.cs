using Core.AnimationsControllers;
using Core.AnimationsSettings;
using Core.Entities;
using Core.Entities.Player.Movement;
using Core.SpriteControllers;
using Core.StateControllers;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Player animations settings")]
        [SerializeField] private Sprite playerIdleSprite;
        [SerializeField] private Sprite playerMovingSprite;
        [SerializeField] private Sprite playerRollLeftSprite;
        [SerializeField] private Sprite playerRollRightSprite;
        [SerializeField] private GameObject player;
        [SerializeField] private float playerMoveInSpeed;
        [SerializeField] private Vector3 playerStartPosition;
        [SerializeField] private Vector3 playerTargetPosition;
        
        [Header("Earth animations settings")]
        [SerializeField] private Transform earth;
        [SerializeField] private float earthMoveOutSpeed;
        [SerializeField] private Vector3 earthStartPosition;
        [SerializeField] private Vector3 earthTargetPosition;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            InstallControllers();
            InstallPlayer();
        }
        
        private void InstallControllers()
        {
            var earthAnimationSettings = new EarthAnimationSettings(
                earth,
                earthMoveOutSpeed,
                earthStartPosition,
                earthTargetPosition);

            var playerAnimationSettings = new PlayerAnimationSettings(
                player,
                playerMoveInSpeed,
                playerStartPosition,
                playerTargetPosition);

            var playerSpriteController = new PlayerSpriteController(
                playerIdleSprite,
                playerMovingSprite,
                playerRollLeftSprite,
                playerRollRightSprite);
            
            var animationController = new AnimationsController(
                earthAnimationSettings, 
                playerAnimationSettings, 
                playerSpriteController);
            
            var playerMover = new PlayerMover(player, playerSpriteController);
            
            Container.BindInstance(playerSpriteController);
            Container.Bind<AnimationsController>().FromInstance(animationController).AsSingle();
            Container.Bind<GameStartController>().FromNew().AsSingle().WithArguments(animationController);
            Container.Bind<GameOverController>().FromNew().AsSingle().WithArguments(animationController);
            Container.Bind<PlayerMover>().FromInstance(playerMover).AsSingle();
            Container.Bind<PlayerInputController>().AsSingle();
        }
        
        private void InstallPlayer()
        {
            Container.Bind<Player>().FromNew().AsSingle();
        }
        
    }
}