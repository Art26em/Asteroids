using Signals;
using Zenject;

namespace Installers
{
    public class SignalsInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameStateChangedSignal>();
            Container.DeclareSignal<LargeAsteroidDestroyedSignal>();
            Container.DeclareSignal<LightEnemyDiedSignal>();
            Container.DeclareSignal<StartAnimationCompletedSignal>();
            Container.DeclareSignal<ScoreIncreasedSignal>();
            Container.DeclareSignal<ObjectDisabledSignal>();
        }
    }
}