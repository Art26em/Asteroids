using Core.StateControllers;
using UI.Views;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameStatesInstaller : MonoInstaller
    {
        [SerializeField] private MobileInputButtonsContainer _buttonsContainer;
        [SerializeField] private GameScreen _gameScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        [SerializeField] private Camera _worldCamera;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_worldCamera).AsSingle();
            Container.Bind<MobileInputButtonsContainer>().FromInstance(_buttonsContainer).AsSingle();
            Container.Bind<GameScreen>().FromInstance(_gameScreen).AsSingle();
            Container.Bind<GameOverScreen>().FromInstance(_gameOverScreen).AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStartController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverController>().AsSingle();
        }
    }
}