using Core.AnimationsControllers;
using Core.AsteroidsLogic;
using Core.EnemiesLogic;
using Zenject;

namespace Core.StateControllers
{
    public class GameStartController
    {
        private AnimationsController _animationsController;
        private AsteroidsController _asteroidsController;
        private EnemiesController _enemiesController;
        
        [Inject]
        private void Construct(
            AnimationsController animationsController, 
            AsteroidsController asteroidsController,
            EnemiesController enemiesController,
            GameScreen gameScreen)
        {
            _animationsController = animationsController;
            _asteroidsController = asteroidsController;
            _enemiesController = enemiesController;
        }
        
        public void StartGame()
        {
            _animationsController.OnGameStart();  
            _asteroidsController.StartAsteroidsSpawning();
            _enemiesController.StartEnemiesSpawning();
        }
        
    }
}