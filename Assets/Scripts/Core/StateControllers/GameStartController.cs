using Core.AnimationsControllers;
using Core.AsteroidsLogic;
using Core.EnemiesLogic;
using Core.States;
using Signals;
using Zenject;

namespace Core.StateControllers
{
    public class GameStartController : IInitializable
    {
        private AnimationsController _animationsController;
        private AsteroidsController _asteroidsController;
        private EnemiesController _enemiesController;
        private GameScreen _gameScreen;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(
            AnimationsController animationsController, 
            AsteroidsController asteroidsController,
            EnemiesController enemiesController,
            SignalBus signalBus,
            GameScreen gameScreen)
        {
            _animationsController = animationsController;
            _asteroidsController = asteroidsController;
            _enemiesController = enemiesController;
            _signalBus = signalBus;
            _gameScreen = gameScreen;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<StartAnimationCompleted>(OnStartAnimationCompleted);
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.NewGameState == GameState.Starting)
            {
                StartGame();
            }
        }

        public void StartGame()
        {
            _animationsController.OnGameStart();  
            _asteroidsController.StartAsteroidsSpawning();
            _enemiesController.StartEnemiesSpawning();
        }

        private void OnStartAnimationCompleted(StartAnimationCompleted _)
        {
            _gameScreen.gameObject.SetActive(true);    
        }
        
    }
}