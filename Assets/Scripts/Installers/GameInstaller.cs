using Core.AnimationsControllers;
using Core.Configs;
using Core.Entities.Asteroids;
using Core.Entities.Asteroids.Movement;
using Core.Entities.Player;
using Core.Entities.Player.Controllers;
using Core.Entities.Player.Fighting.Projectiles;
using Core.Entities.Player.Fighting.Weapons;
using Core.Entities.Player.Movement;
using Core.Factories;
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
        [SerializeField] private GameObject playerObject;
        [SerializeField] private float playerMoveInSpeed;
        [SerializeField] private Vector2 playerStartPosition;
        [SerializeField] private Vector2 playerTargetPosition;
        
        [Header("Space animations settings")]
        [SerializeField] private Transform earth;
        [SerializeField] private float earthMoveOutSpeed;
        [SerializeField] private Vector3 earthStartPosition;
        [SerializeField] private Vector3 earthTargetPosition;
        [SerializeField] private ParticleSystem space;
        
        [Header("Asteroids settings")]
        [SerializeField] private LargeAsteroid largeAsteroidPrefab;
        [SerializeField] private MediumAsteroid mediumAsteroidPrefab;
        [SerializeField] private SmallAsteroid smallAsteroidPrefab;
        [SerializeField] private GameObject asteroidsContainer;
        [SerializeField] private Transform[] asteroidSpawnPositions;
        
        [Header("Weapons settings")]
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform bulletsContainer;
        [SerializeField] private Transform shootPoint;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            var worldBoundsChecker = new WorldBoundsChecker();
            
            // Asteroids settings
            var asteroidsConfigManager = new ConfigManager<AsteroidsData>();
            var asteroidsData = asteroidsConfigManager.LoadConfigs(ConfigsSettings.AsteroidsConfigName);
            
            var asteroidMover = new AsteroidMover(asteroidsData, worldBoundsChecker);
            
            var asteroidFactory = new AsteroidFactory(
                largeAsteroidPrefab,
                mediumAsteroidPrefab,
                smallAsteroidPrefab,
                asteroidsContainer, 
                asteroidsData.AsteroidPoolSize);
            
            var asteroidSpawner = new AsteroidSpawner(
                asteroidFactory, 
                asteroidMover, 
                asteroidSpawnPositions,
                asteroidsData.TimeToSpawn);
            
            Container.Bind<AsteroidsData>().FromInstance(asteroidsData).AsSingle();
            Container.Bind<AsteroidMover>().FromInstance(asteroidMover).AsSingle();
            Container.Bind<AsteroidFactory>().FromInstance(asteroidFactory).AsSingle();
            Container.Bind<AsteroidSpawner>().FromInstance(asteroidSpawner).AsSingle();  
            
            // Weapons settings
            var projectilesConfigManager = new ConfigManager<ProjectilesData>();
            var projectData = projectilesConfigManager.LoadConfigs(ConfigsSettings.ProjectilesConfigName);
            var blasters = new Blasters(bulletPrefab, projectData.MagazineSize, bulletsContainer, shootPoint);
            Container.Bind<Blasters>().FromInstance(blasters).AsSingle();
            
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
                playerSpriteRenderer);
            
            var animationController = new AnimationsController(
                earthAnimationSettings, 
                playerAnimationSettings, 
                playerSpriteController,
                space);
            
            var playerMover = new PlayerMover(playerObject, playerStats, worldBoundsChecker);
            
            Container.BindInstance(playerSpriteController);
            Container.Bind<PlayerStats>().FromInstance(playerStats).AsSingle();
            Container.Bind<AnimationsController>().FromInstance(animationController).AsSingle();
            Container.Bind<GameStartController>().AsSingle().WithArguments(animationController, asteroidSpawner);
            Container.Bind<GameOverController>().AsSingle().WithArguments(animationController);
            Container.Bind<PlayerMover>().FromInstance(playerMover).AsSingle();
            Container.Bind<WorldBoundsChecker>().FromInstance(worldBoundsChecker).AsSingle();
            Container.Bind<PlayerInputController>().AsSingle();
        }
        
    }
}