using System;
using Core.AnimationsControllers;
using Core.AsteroidsLogic;
using Core.EnemiesLogic;
using Signals;
using UI.Views;
using UnityEngine;
using Zenject;

namespace Core.StateControllers
{
    public class GameStartController : IInitializable, IDisposable
    {
        private AnimationsController _animationsController;
        private AsteroidsController _asteroidsController;
        private EnemiesController _enemiesController;
        private MobileInputButtonsContainer _buttonsContainer;
        private GameScreen _gameScreen;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(
            AnimationsController animationsController, 
            AsteroidsController asteroidsController,
            EnemiesController enemiesController,
            MobileInputButtonsContainer buttonsContainer,
            GameScreen gameScreen,
            SignalBus signalBus)
        {
            _animationsController = animationsController;
            _asteroidsController = asteroidsController;
            _enemiesController = enemiesController;
            _buttonsContainer = buttonsContainer;
            _gameScreen = gameScreen;
            _signalBus = signalBus;
        }
        
        public void StartGame()
        {
            _animationsController.OnGameStart();  
            _asteroidsController.StartAsteroidsSpawning();
            _enemiesController.StartEnemiesSpawning();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<StartAnimationCompletedSignal>(OnStartAnimationCompleted);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<StartAnimationCompletedSignal>(OnStartAnimationCompleted);
        }
        
        private void OnStartAnimationCompleted(StartAnimationCompletedSignal _)
        {
            _gameScreen.gameObject.SetActive(true); 
            _buttonsContainer.gameObject.SetActive(Application.isMobilePlatform);
        }
        
    }
}