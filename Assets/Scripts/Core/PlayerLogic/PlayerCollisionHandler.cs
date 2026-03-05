using Core.AsteroidsPresentation;
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
            if  (other.gameObject.TryGetComponent<Asteroid>(out _))
            {
               _playerStats.HealthStats.DecreaseHealth();
            }
        }
    }
}
