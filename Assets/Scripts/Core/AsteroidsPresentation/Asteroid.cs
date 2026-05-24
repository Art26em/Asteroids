using UnityEngine;
using Zenject;

namespace Core.AsteroidsPresentation
{
    public abstract class Asteroid : MonoBehaviour
    {
        [SerializeField] protected ParticleSystem explosionEffect;
        protected SignalBus SignalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }

        protected void PlayExplosionEffect()
        {
            ParticleSystem explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration + explosion.main.startLifetime.constant);
        }
        
    }
}