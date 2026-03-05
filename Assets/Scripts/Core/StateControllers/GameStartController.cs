using Core.AnimationsControllers;
using Core.AsteroidsLogic;
using Zenject;

namespace Core.StateControllers
{
    public class GameStartController
    {
        private AnimationsController _animationsController;
        private AsteroidsController _asteroidsController;

        [Inject]
        private void Construct(
            AnimationsController animationsController, 
            AsteroidsController asteroidsController)
        {
            _animationsController = animationsController;
            _asteroidsController = asteroidsController;
        }
        
        public void StartGame()
        {
            _animationsController.OnGameStart();  
            _asteroidsController.StartAsteroidsSpawning();
        }
    }
}