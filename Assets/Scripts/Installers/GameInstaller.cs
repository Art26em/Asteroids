using Core.AnimationsControllers;
using Core.AnimationsSettings;
using Core.Configs;
using Core.Entities.Player;
using Core.Entities.Player.Controllers;
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
        [SerializeField] private GameObject playerObject;
        [SerializeField] private float playerMoveInSpeed;
        [SerializeField] private Vector2 playerStartPosition;
        [SerializeField] private Vector2 playerTargetPosition;
        
        [Header("Earth animations settings")]
        [SerializeField] private Transform earth;
        [SerializeField] private float earthMoveOutSpeed;
        [SerializeField] private Vector2 earthStartPosition;
        [SerializeField] private Vector2 earthTargetPosition;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            InstallControllers();
			InstallEnemies();
        }
        
        private void InstallControllers()
        {
            var earthAnimationSettings = new EarthAnimationSettings(
                earth,
                earthMoveOutSpeed,
                earthStartPosition,
                earthTargetPosition);

            var playerConfigLoader = new PlayerConfigLoader(new PlayerData());
            var playerStats = new PlayerStats(playerConfigLoader);
            
            var playerAnimationSettings = new PlayerAnimationSettings(
                playerObject,
                playerMoveInSpeed,
                playerStartPosition,
                playerTargetPosition);

            var playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();
            var playerSpriteController = new PlayerSpriteController(
                playerIdleSprite,
                playerMovingSprite,
                playerRollLeftSprite,
                playerRollRightSprite,
                playerSpriteRenderer);
            
            var animationController = new AnimationsController(
                earthAnimationSettings, 
                playerAnimationSettings, 
                playerSpriteController);
            
            var playerMover = new PlayerMover(playerObject, playerStats);
            
            Container.BindInstance(playerSpriteController);
            Container.Bind<PlayerStats>().FromInstance(playerStats).AsSingle();
            Container.Bind<AnimationsController>().FromInstance(animationController).AsSingle();
            Container.Bind<GameStartController>().FromNew().AsSingle().WithArguments(animationController);
            Container.Bind<GameOverController>().FromNew().AsSingle().WithArguments(animationController);
            Container.Bind<PlayerMover>().FromInstance(playerMover).AsSingle();
            Container.Bind<PlayerInputController>().AsSingle();
        }
        
		private void InstallEnemies()
		{
			// Some logic
		}
    }
}