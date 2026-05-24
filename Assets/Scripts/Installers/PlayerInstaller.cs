using Core.ObjectMovers;
using Core.PlayerLogic;
using Core.PlayerPresentation;
using Core.ScoreSystem;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<Score>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle();
            Container.Bind<PlayerMover>().AsSingle();
            Container.Bind<PlayerInputController>().FromComponentInHierarchy().AsSingle();
        }
    }
}