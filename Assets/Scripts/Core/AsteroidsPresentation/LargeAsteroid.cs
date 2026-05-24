using Core.Configs;
using Core.Physics;
using Core.ProjectilesPresentation;
using Core.SpeedSystem;
using Core.World;
using Signals;
using UnityEngine;
using Zenject;

namespace Core.AsteroidsPresentation
{
    public class LargeAsteroid : Asteroid
    {
        public SpeedStats SpeedStats;
        private SignalBus _signalBus;
        private int _score;

        [Inject]
        private void Construct(AsteroidsData asteroidsData, SignalBus signalBus)
        {
            SpeedStats = new SpeedStats
            {
                MaxSpeed = asteroidsData.LargeAsteroidSpeedStats.MaxSpeed,
                Acceleration = asteroidsData.LargeAsteroidSpeedStats.Acceleration,
                Deceleration = asteroidsData.LargeAsteroidSpeedStats.Deceleration,
                RotationSpeed = asteroidsData.LargeAsteroidSpeedStats.RotationSpeed
            };
            _signalBus = signalBus;
            _score = asteroidsData.LargeAsteroidScore;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out WorldBoundsChecker _)) return;
            
            if (other.gameObject.TryGetComponent(out Projectile _))
            {
                _signalBus.Fire(new LargeAsteroidDestroyedSignal(other.gameObject.transform));
                _signalBus.Fire(new ScoreIncreasedSignal(_score));

                PlayExplosionEffect();
                gameObject.SetActive(false);
            }
            else
            {
                var bounceDirection = CollisionPhysics.GetBounceDirection(SpeedStats, other);
                SpeedStats.CurrentVelocity = bounceDirection * SpeedStats.CurrentSpeed;
            }
        }
        
    }
}