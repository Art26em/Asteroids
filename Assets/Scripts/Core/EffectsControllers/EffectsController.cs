using UnityEngine;

namespace Core.EffectsControllers
{
    public class EffectsController
    {
        public ParticleSystem AsteroidExplosionEffect;
        public ParticleSystem EnemyExplosionEffect;
        public ParticleSystem PlayerInvulnerabilityEffect;

        public EffectsController(
            ParticleSystem asteroidExplosionEffect, 
            ParticleSystem enemyExplosionEffect, 
            ParticleSystem playerInvulnerabilityEffect)
        {
            AsteroidExplosionEffect = asteroidExplosionEffect;
            EnemyExplosionEffect = enemyExplosionEffect;
            PlayerInvulnerabilityEffect = playerInvulnerabilityEffect;
        }
    }
}