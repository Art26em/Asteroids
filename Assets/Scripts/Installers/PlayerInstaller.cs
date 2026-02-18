using Core.Configs;
using Core.Entities.Player;
using Core.Entities.Player.Controllers;
using Core.Entities.Player.Movement;
using Zenject;

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
        Container.Bind<PlayerInputController>().AsSingle();
    }
}