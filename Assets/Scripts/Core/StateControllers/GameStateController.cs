using Zenject;

namespace Core.StateControllers
{

    public class GameStateController
    {
        private GameStartController _gameStartController;
        private GameOverController _gameOverController;

        [Inject]
        private void Construct(GameStartController gameStartController, GameOverController gameOverController)
        {
            _gameStartController = gameStartController;
            _gameOverController = gameOverController;
        }
        
        public void StartGame()
        {
            _gameStartController.StartGame();
        }

        public void StopGame()
        {
            
        }
        
    }
}