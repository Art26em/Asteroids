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

        [Inject]
        private void Construct(AsteroidsData asteroidsData)
        {
            SpeedStats = new SpeedStats
            {
                MaxSpeed = asteroidsData.LargeAsteroidSpeedStats.MaxSpeed,
                Acceleration = asteroidsData.LargeAsteroidSpeedStats.Acceleration,
                Deceleration = asteroidsData.LargeAsteroidSpeedStats.Deceleration,
                RotationSpeed = asteroidsData.LargeAsteroidSpeedStats.RotationSpeed
            };
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out WorldBoundsChecker _)) return;
            
            if (other.gameObject.TryGetComponent(out Projectile _))
            {
                SignalBus.Fire(new LargeAsteroidDestroyedSignal(other.gameObject.transform));
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