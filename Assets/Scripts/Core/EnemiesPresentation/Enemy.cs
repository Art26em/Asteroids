using UnityEngine;

namespace Core.EnemiesPresentation
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected ParticleSystem explosionEffect;
        
        protected void PlayExplosionEffect()
        {
            ParticleSystem explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration + explosion.main.startLifetime.constant);
        } 
    }
}