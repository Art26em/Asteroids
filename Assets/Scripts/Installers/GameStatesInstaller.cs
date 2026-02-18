using Core.StateControllers;
using Zenject;

public class GameStatesInstaller : MonoInstaller
{
    // ReSharper disable Unity.PerformanceAnalysis
    public override void InstallBindings()
    {
        Container.Bind<GameStartController>().AsSingle();
        Container.Bind<GameOverController>().AsSingle();
    }
}