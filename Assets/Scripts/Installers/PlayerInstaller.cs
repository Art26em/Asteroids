using Core.ObjectMovers;
using Core.PlayerLogic;
using Core.PlayerPresentation;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<PlayerStats>().AsSingle();
            Container.Bind<PlayerMover>().AsSingle();
            Container.Bind<PlayerInputController>().FromComponentInHierarchy().AsSingle();
        }
    }
}