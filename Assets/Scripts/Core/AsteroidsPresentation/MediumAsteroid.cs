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
    public class MediumAsteroid : Asteroid
    {
        public SpeedStats SpeedStats;
        private SignalBus _signalBus;
        private int _score;

        [Inject]
        private void Construct(AsteroidsData asteroidsData, SignalBus signalBus)
        {
            SpeedStats = new SpeedStats
            {
                MaxSpeed = asteroidsData.MediumAsteroidSpeedStats.MaxSpeed,
                Acceleration = asteroidsData.MediumAsteroidSpeedStats.Acceleration,
                Deceleration = asteroidsData.MediumAsteroidSpeedStats.Deceleration,
                RotationSpeed = asteroidsData.MediumAsteroidSpeedStats.RotationSpeed
            };
            _signalBus = signalBus;
            _score = asteroidsData.MediumAsteroidScore;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out WorldBoundsChecker _)) return;
            
            if (other.gameObject.TryGetComponent(out Projectile _))
            {
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