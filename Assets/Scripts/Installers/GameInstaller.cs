using Core.AnimationsControllers;
using Core.AnimationsSettings;
using Core.Configs;
using Core.Entities;
using Core.Entities.Physics;
using Core.Entities.Player;
using Core.Entities.Player.Movement;
using Core.SpriteControllers;
using Core.StateControllers;
using UnityEngine;
using Zenject;

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
            InstallPlayer();
            InstallControllers();
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
            
            var playerMover = new PlayerMover(player, playerSpriteController, new MovementPhysics());
            
            Container.BindInstance(playerSpriteController);
            Container.Bind<AnimationsController>().FromInstance(animationController).AsSingle();
            Container.Bind<GameStartController>().FromNew().AsSingle().WithArguments(animationController);
            Container.Bind<GameOverController>().FromNew().AsSingle().WithArguments(animationController);
            Container.Bind<PlayerMover>().FromInstance(playerMover).AsSingle();
            Container.Bind<PlayerInputController>().AsSingle();
        }
        
        private void InstallPlayer()
        {
            var playerConfigLoader = new PlayerConfigLoader(new PlayerData());
            Container.Bind<PlayerStats>().FromInstance(new PlayerStats(playerConfigLoader)).AsSingle();
        }
        
    }
}