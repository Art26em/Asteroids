using Core.EffectsControllers;
using Core.Physics;
using Core.ProjectilesPresentation;
using Core.SpeedSystem;
using Core.World;
using UnityEngine;
using Zenject;

namespace Core.AsteroidsPresentation
{
    public abstract class Asteroid : MonoBehaviour
    {
        protected EffectsController EffectsController;
        public SpeedStats SpeedStats;
        protected int Score;
        protected SignalBus SignalBus;

        [Inject]
        private void Construct(SignalBus signalBus, EffectsController effectsController)
        {
            SignalBus = signalBus;
            EffectsController = effectsController;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out WorldBoundsChecker _)) return;
            
            if (other.gameObject.TryGetComponent(out Projectile _))
            {
                FireSignals(other);
                EffectsController.ExplodeAsteroid(transform);
                gameObject.SetActive(false);
            }
            else
            {
                var bounceDirection = CollisionPhysics.GetBounceDirection(SpeedStats, other);
                SpeedStats.CurrentVelocity = bounceDirection * SpeedStats.CurrentSpeed;
            }
        }

        protected virtual void FireSignals(Collision2D other) {}

    }
}