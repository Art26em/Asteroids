using Core.AsteroidsPresentation;
using Core.EnemiesPresentation;
using Core.Physics;
using Core.PlayerPresentation;
using Core.States;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using Zenject;

namespace Core.PlayerLogic
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private PlayerStats _playerStats;
        private PlayerObject _playerObject;
        private PlayerInputController _playerInputController;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(
            PlayerStats playerStats, 
            PlayerObject playerObject,
            PlayerInputController inputController, 
            SignalBus signalBus)
        {
            _playerStats = playerStats;
            _playerObject = playerObject;
            _playerInputController = inputController;
            _signalBus = signalBus;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent<Asteroid>(out _) &&
                !other.gameObject.TryGetComponent<Enemy>(out _)) return;
            
            var bounceDirection= CollisionPhysics.GetBounceDirection(_playerStats.SpeedStats, other);
            _playerStats.SpeedStats.CurrentVelocity = bounceDirection * _playerStats.SpeedStats.CurrentSpeed;

            if (!_playerStats.IsImmortal)
            {
                _playerStats.HealthStats.DecreaseHealth();
                Debug.Log("Collision " + other.gameObject.name);
                if (_playerStats.HealthStats.IsDead())
                {
                    _signalBus.Fire(new GameStateChangedSignal(GameState.GameOver));
                    gameObject.SetActive(false);
                }
                else
                {
                    _ = ActivateInvulnerability();
                }   
            }
            
        }

        private async UniTask ActivateInvulnerability()
        {
            _playerStats.IsImmortal = true;
            _playerObject.PlayInvulnerabilityEffect();
            var elapsedTime = 0.0f;
            while (elapsedTime < _playerStats.InvincibilityTime)
            {
                elapsedTime += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            _playerStats.IsImmortal = false;
            _playerObject.StopInvulnerabilityEffect();
        }
        
    }
}
