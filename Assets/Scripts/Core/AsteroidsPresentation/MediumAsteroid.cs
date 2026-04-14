using Core.Configs;
using Core.Physics;
using Core.ProjectilesPresentation;
using Core.SpeedSystem;
using Core.World;
using UnityEngine;
using Zenject;

namespace Core.AsteroidsPresentation
{
    public class MediumAsteroid : Asteroid
    {
        public SpeedStats SpeedStats;

        [Inject]
        private void Construct(AsteroidsData asteroidsData)
        {
            SpeedStats = new SpeedStats
            {
                MaxSpeed = asteroidsData.MediumAsteroidSpeedStats.MaxSpeed,
                Acceleration = asteroidsData.MediumAsteroidSpeedStats.Acceleration,
                Deceleration = asteroidsData.MediumAsteroidSpeedStats.Deceleration,
                RotationSpeed = asteroidsData.MediumAsteroidSpeedStats.RotationSpeed
            };
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out WorldBoundsChecker _)) return;
            
            if (other.gameObject.TryGetComponent(out Projectile _))
            {
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