using Core.Configs;
using Core.HealthSystem;
using Core.Physics;
using Core.ProjectilesPresentation;
using Core.SpeedSystem;
using Core.World;
using Signals;
using UnityEngine;
using Zenject;

namespace Core.EnemiesPresentation
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class LightEnemy : Enemy
    {
        private HealthStats healthStats;
        public SpeedStats SpeedStats;
        private int _score;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(EnemiesData enemiesData, SignalBus signalBus)
        {
            healthStats = new HealthStats
            {
                MaxHealth = enemiesData.LightEnemyHealthStats.MaxHealth,
                CurrentHealth = enemiesData.LightEnemyHealthStats.CurrentHealth
            };
            SpeedStats = new SpeedStats
            {
                MaxSpeed = enemiesData.LightEnemySpeedStats.MaxSpeed,
                Acceleration = enemiesData.LightEnemySpeedStats.Acceleration,
                Deceleration = enemiesData.LightEnemySpeedStats.Deceleration,
                RotationSpeed = enemiesData.LightEnemySpeedStats.RotationSpeed
            };
            _signalBus = signalBus;
            _score = enemiesData.LightEnemyScore;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out WorldBoundsChecker _)) return;
            
            if (collision.gameObject.TryGetComponent(out Projectile _))
            {
                healthStats.DecreaseHealth();
                if (healthStats.IsDead())
                {
                    Die();
                }
            } else
            {
                var bounceDirection = CollisionPhysics.GetBounceDirection(SpeedStats, collision);
                SpeedStats.CurrentVelocity = bounceDirection * SpeedStats.CurrentSpeed;
            }   
        }
        
        private void Die()
        {
            _signalBus.Fire<LightEnemyDiedSignal>();
            _signalBus.Fire(new ScoreIncreasedSignal(_score));

            PlayExplosionEffect();
            gameObject.SetActive(false);     
        }   
    }
}