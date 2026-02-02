using Core.AnimationsControllers;
using Core.Spawners;

namespace Core.StateControllers
{
    public class GameStartController
    {
        private readonly AnimationsController _animationsController;
        private readonly AsteroidSpawner _asteroidSpawner;
        
        public GameStartController(AnimationsController animationsController)
        {
            _animationsController = animationsController;
        }

        public void StartGame()
        {
            _animationsController.OnGameStart();    
        }
        
    }
}