using Core.AnimationsControllers;
using Core.Configs;
using Core.Entities.Asteroids;
using Core.Entities.Asteroids.Movement;
using Core.Entities.Player;
using Core.Entities.Player.Controllers;
using Core.Entities.Player.Movement;
using Core.Factories;
using Core.ObjectPools;
using Core.Spawners;
using Core.SpriteControllers;
using Core.StateControllers;
using Core.World;
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
        [SerializeField] private Vector3 earthStartPosition;
        [SerializeField] private Vector3 earthTargetPosition;
        
        [Header("Asteroids settings")]
        [SerializeField] private GameObject[] largeAsteroidPrefabs;
        [SerializeField] private GameObject[] mediumAsteroidPrefabs;
        [SerializeField] private GameObject[] smallAsteroidPrefabs;
        [SerializeField] private GameObject asteroidsContainer;
        [SerializeField] private Transform[] asteroidSpawnPositions;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            // Asteroids settings
            var asteroidsConfigManager = new ConfigManager<AsteroidsData>();
            var asteroidsData = asteroidsConfigManager.LoadConfigs(ConfigsSettings.AsteroidsConfigName);

            // var asteroidPool = new ObjectPool<Asteroid>(
            //     asteroidsData.AsteroidPoolSize,);
            
            var asteroidMover = new AsteroidMover(
                asteroidsData.MovingSpeedX, 
                asteroidsData.MovingSpeedY, 
                asteroidsData.RotationSpeed);
            
            var asteroidFactory = new AsteroidFactory(
                largeAsteroidPrefabs,
                mediumAsteroidPrefabs,
                smallAsteroidPrefabs,
                asteroidsContainer);
            
            var asteroidSpawner = new AsteroidSpawner(
                asteroidFactory, 
                asteroidMover, 
                asteroidSpawnPositions,
                asteroidsData.TimeToSpawn);
            
            Container.Bind<AsteroidsData>().FromInstance(asteroidsData).AsSingle();
            Container.Bind<AsteroidMover>().FromInstance(asteroidMover).AsSingle();
            Container.Bind<AsteroidFactory>().FromInstance(asteroidFactory).AsSingle();
            Container.Bind<AsteroidSpawner>().FromInstance(asteroidSpawner).AsSingle();  
            
            var earthAnimationSettings = new EarthAnimationSettings(
                earth,
                earthMoveOutSpeed,
                earthStartPosition,
                earthTargetPosition);

            var playerConfigManager = new ConfigManager<PlayerData>();
            var playerData = playerConfigManager.LoadConfigs(ConfigsSettings.PlayerConfigName);
            var playerStats = new PlayerStats(playerData);
            
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
            Container.Bind<GameStartController>().AsSingle().WithArguments(animationController, asteroidSpawner);
            Container.Bind<GameOverController>().AsSingle().WithArguments(animationController);
            Container.Bind<PlayerMover>().FromInstance(playerMover).AsSingle();
            Container.Bind<WorldBoundsChecker>().AsSingle();
            Container.Bind<PlayerInputController>().AsSingle();
        }
        
    }
}