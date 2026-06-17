using Core.PlayerPresentation;
using Core.States;
using Signals;
using Zenject;

namespace Core.PlayerLogic
{
    public class PlayerDamageHandler
    {
        private PlayerObject _playerObject;
        private PlayerInvulnerabilityController _playerInvulnerabilityController;
        private PlayerStats _playerStats;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(
            PlayerObject playerObject, 
            PlayerStats playerStats, 
            SignalBus signalBus,
            PlayerInvulnerabilityController playerInvulnerabilityController)
        {
            _playerObject = playerObject;
            _playerStats = playerStats;
            _signalBus = signalBus;
            _playerInvulnerabilityController = playerInvulnerabilityController;
        }

        public void HandleDamage()
        {
            if (!_playerStats.IsImmortal)
            {
                _playerStats.HealthStats.DecreaseHealth();
                if (_playerStats.HealthStats.IsDead())
                {
                    _signalBus.Fire(new GameStateChangedSignal(GameState.GameOver));
                    _playerObject.gameObject.SetActive(false);
                }
                else
                {
                    _playerInvulnerabilityController.StartInvulnerability();
                }   
            }
        }
        
    }
}