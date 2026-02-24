using UnityEngine;

namespace Core.AsteroidsPresentation
{
    public abstract class Asteroid : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleCollision(other.gameObject);
        }
        
        protected virtual void HandleCollision(GameObject other)
        {
            
        }
    }
}