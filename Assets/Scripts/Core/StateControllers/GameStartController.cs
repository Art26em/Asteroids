using Core.AnimationsControllers;
using Core.Entities.Asteroids;
using Core.Spawners;

namespace Core.StateControllers
{
    public class GameStartController
    {
        private readonly AnimationsController _animationsController;
        private readonly ObjectSpawner<LargeAsteroid> _asteroidSpawner;
        
        public GameStartController(AnimationsController animationsController, ObjectSpawner<LargeAsteroid> asteroidSpawner)
        {
            _animationsController = animationsController;
            _asteroidSpawner = asteroidSpawner;
        }

        public void StartGame()
        {
            _animationsController.OnGameStart();  
            _asteroidSpawner.StartObjectsSpawning();
        }
        
    }
}