using Core.ObjectMovers;
using Core.PlayerLogic;
using Core.PlayerPresentation;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<Score>().AsSingle();
            Container.Bind<PlayerStats>().AsSingle();
            Container.Bind<PlayerMover>().AsSingle();
            Container.Bind<PlayerInputController>().FromComponentInHierarchy().AsSingle();
        }
    }
}