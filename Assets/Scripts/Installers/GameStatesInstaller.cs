using Core.StateControllers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameStatesInstaller : MonoInstaller
    {
        [SerializeField] private GameScreen gameScreen;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<GameScreen>().FromInstance(gameScreen).AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStartController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverController>().AsSingle();
        }
    }
}