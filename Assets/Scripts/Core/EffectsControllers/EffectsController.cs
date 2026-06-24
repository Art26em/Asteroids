using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.EffectsControllers
{
    public class EffectsController
    {
        public readonly ParticleSystem AsteroidExplosionEffect;
        public readonly ParticleSystem EnemyExplosionEffect;
        public readonly ParticleSystem PlayerInvulnerabilityEffect;
        
        public EffectsController(
            ParticleSystem asteroidExplosionEffect, 
            ParticleSystem enemyExplosionEffect, 
            ParticleSystem playerInvulnerabilityEffect)
        {
            AsteroidExplosionEffect = asteroidExplosionEffect;
            EnemyExplosionEffect = enemyExplosionEffect;
            PlayerInvulnerabilityEffect = playerInvulnerabilityEffect;
        }

        public void ExplodeAsteroid(Transform transform)
        {
            PlayExplosionEffect(AsteroidExplosionEffect, transform.position);    
        }
        
        public void ExplodeEnemy(Transform transform)
        {
            PlayExplosionEffect(EnemyExplosionEffect, transform.position);    
        }
        
        private void PlayExplosionEffect(ParticleSystem effect, Vector3 position)
        {
            var explosion = Object.Instantiate(effect, position, Quaternion.identity);
            explosion.Play();
            Object.Destroy(explosion.gameObject, explosion.main.duration + explosion.main.startLifetime.constant);
        } 
        
    }
}