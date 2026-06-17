using System;
using Core.States;
using Signals;
using UnityEngine;
using Zenject;

namespace Core.StateControllers
{
    public class GameStateController : IInitializable, IDisposable
    {
        private GameStartController _gameStartController;
        private GameOverController _gameOverController;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(
            GameStartController gameStartController, 
            GameOverController gameOverController,
            GameScreen gameScreen,
            SignalBus signalBus)
        {
            _gameStartController = gameStartController;
            _gameOverController = gameOverController;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        public void Dispose() 
        {
            _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }
        
        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.NewGameState == GameState.Starting)
            {
                _gameStartController.StartGame();
                Time.timeScale = 1;
            }
            else if (signal.NewGameState == GameState.Paused)
            {
                Time.timeScale = 0;    
            }
            else if (signal.NewGameState == GameState.GameOver)
            {
                _gameOverController.OnGameOver();
                Time.timeScale = 0;
            }
        }
        
    }
}