using Core.Configs;
using Core.HealthSystem;
using Core.ProjectilesPresentation;
using Core.SpeedSystem;
using UnityEngine;
using Zenject;

namespace Core.EnemiesPresentation
{
    public class LightEnemy : Enemy
    {
        private HealthStats _healthStats;
        private SpeedStats _speedStats;
        
        [Inject]
        private void Construct(EnemiesData enemiesData)
        {
            _healthStats = enemiesData.HealthStats;
            _speedStats = enemiesData.SpeedStats;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet _))
            {
                _healthStats.DecreaseHealth(1);
            }
        }
        
        private void Die()
        {
             
        }   
    }
}