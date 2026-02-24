using Core.StateControllers;
using Zenject;

namespace Installers
{
    public class GameStatesInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<GameStateController>().AsSingle();
            Container.Bind<GameStartController>().AsSingle();
            Container.Bind<GameOverController>().AsSingle();
        }
    }
}