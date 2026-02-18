using Core.Configs;
using Core.Entities.Health;
using Core.Entities.Player.Fighting.Projectiles;
using Core.Entities.Speed;
using UnityEngine;
using Zenject;

namespace Core.Entities.Enemies
{
    public class EnemyObject : MonoBehaviour
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
            if (other.TryGetComponent(out Bullet bullet))
            {
                Die();
            }
        }

        private void Die()
        {
            _healthStats.DecreaseHealth(1);    
        }
        
    }
}