using Core.AsteroidsPresentation;
using Core.EnemiesPresentation;
using Core.Physics;
using Core.PlayerPresentation;
using UnityEngine;
using Zenject;

namespace Core.PlayerLogic
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private PlayerStats _playerStats;
        
        [Inject]
        private void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Asteroid>(out _) || other.gameObject.TryGetComponent<Enemy>(out _))
            {
               _playerStats.HealthStats.DecreaseHealth();
               var bounceDirection= CollisionPhysics.GetBounceDirection(_playerStats.SpeedStats, other);
               _playerStats.SpeedStats.CurrentVelocity = bounceDirection * _playerStats.SpeedStats.CurrentSpeed;
            }
        }
    }
}
