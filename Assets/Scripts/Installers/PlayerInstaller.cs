using Core.Configs;
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
            var playerConfigManager = new ConfigManager<PlayerData>();
            var playerData = playerConfigManager.LoadConfigs(ConfigsSettings.PlayerConfigName);
            
            Container.Bind<PlayerData>().FromInstance(playerData).AsSingle();
            Container.Bind<PlayerStats>().AsSingle();
            Container.Bind<PlayerMover>().AsSingle();
            Container.Bind<PlayerInputController>().FromComponentInHierarchy().AsSingle();
        }
    }
}