using Core.AnimationsControllers;
using Core.AsteroidsPresentation;
using Core.Spawners;
using Zenject;

namespace Core.StateControllers
{
    public class GameStartController
    {
        private AnimationsController _animationsController;
        private ObjectSpawner<LargeAsteroid> _asteroidSpawner;

        [Inject]
        private void Construct(AnimationsController animationsController, ObjectSpawner<LargeAsteroid> asteroidSpawner)
        {
            _animationsController = animationsController;
            _asteroidSpawner = asteroidSpawner;
        }
        
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