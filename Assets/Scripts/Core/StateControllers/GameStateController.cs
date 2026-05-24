using System;
using Core.ScoreSystem;
using Core.States;
using Signals;
using Zenject;

namespace Core.StateControllers
{

    public class GameStateController : IInitializable, IDisposable
    {
        private GameStartController _gameStartController;
        private GameOverController _gameOverController;
        private GameScreen _gameScreen;
        private Score _score;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(
            GameStartController gameStartController, 
            GameOverController gameOverController,
            GameScreen gameScreen,
            Score score,
            SignalBus signalBus)
        {
            _gameStartController = gameStartController;
            _gameOverController = gameOverController;
            _gameScreen = gameScreen;
            _score = score;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
            _signalBus.Subscribe<StartAnimationCompleted>(OnStartAnimationCompleted);
            _signalBus.Subscribe<ScoreIncreasedSignal>(OnScoreIncreased);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.NewGameState == GameState.Starting)
            {
                _gameStartController.StartGame();        
            }
            else if (signal.NewGameState == GameState.GameOver)
            {
                _gameOverController.OnGameOver();
            }
        }
        
        private void OnScoreIncreased(ScoreIncreasedSignal signal)
        {
            _score.AddScore(signal.AddedScore);
        }
        
        private void OnStartAnimationCompleted(StartAnimationCompleted _)
        {
            _gameScreen.gameObject.SetActive(true);    
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
            _signalBus.Unsubscribe<StartAnimationCompleted>(OnStartAnimationCompleted);
            _signalBus.Unsubscribe<ScoreIncreasedSignal>(OnScoreIncreased);
        }
    }
}