using Core.AnimationsControllers;
using Core.Spawners;

namespace Core.StateControllers
{
    public class GameStartController
    {
        private readonly AnimationsController _animationsController;
        private readonly AsteroidSpawner _asteroidSpawner;
        
        public GameStartController(AnimationsController animationsController, AsteroidSpawner asteroidSpawner)
        {
            _animationsController = animationsController;
            _asteroidSpawner = asteroidSpawner;
        }

        public void StartGame()
        {
            _animationsController.OnGameStart();  
            _asteroidSpawner.StartSpawning();
        }
        
    }
}