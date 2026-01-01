using Core.AnimationsControllers;

namespace Core.StateControllers
{
    public class GameStartController
    {
        private readonly AnimationsController _animationsController;
        
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