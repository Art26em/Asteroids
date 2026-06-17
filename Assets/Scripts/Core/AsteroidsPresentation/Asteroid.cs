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
        private EffectsController _effectsController;
        public SpeedStats SpeedStats;
        protected int Score;
        protected SignalBus SignalBus;

        [Inject]
        private void Construct(SignalBus signalBus, EffectsController effectsController)
        {
            SignalBus = signalBus;
            _effectsController = effectsController;
        }

        private void PlayExplosionEffect()
        {
            var explosion = Instantiate(
                _effectsController.AsteroidExplosionEffect, transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration + explosion.main.startLifetime.constant);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out WorldBoundsChecker _)) return;
            
            if (other.gameObject.TryGetComponent(out Projectile _))
            {
                FireSignals(other);
                PlayExplosionEffect();
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