using Core.EffectsControllers;
using UnityEngine;
using Zenject;

namespace Core.EnemiesPresentation
{
    public abstract class Enemy : MonoBehaviour
    {
        private EffectsController _effectsController;

        [Inject]
        private void Construct(EffectsController effectsController)
        {
            _effectsController = effectsController;
        }
        
        protected void PlayExplosionEffect()
        {
            var explosion = Instantiate(
                _effectsController.EnemyExplosionEffect, transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration + explosion.main.startLifetime.constant);
        } 
    }
}