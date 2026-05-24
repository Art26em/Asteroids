using Core.StateControllers;
using UI.Views;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameStatesInstaller : MonoInstaller
    {
        [SerializeField] private GameScreen gameScreen;
        [SerializeField] private GameOverScreen gameOverScreen;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<GameScreen>().FromInstance(gameScreen).AsSingle();
            Container.Bind<GameOverScreen>().FromInstance(gameOverScreen).AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStartController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverController>().AsSingle();
        }
    }
}