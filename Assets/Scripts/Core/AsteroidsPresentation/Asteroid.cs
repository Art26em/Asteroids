using UnityEngine;
using Zenject;

namespace Core.AsteroidsPresentation
{
    public abstract class Asteroid : MonoBehaviour
    {
        protected SignalBus SignalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleCollision(other.gameObject);
        }
        
        protected virtual void HandleCollision(GameObject other) {}
    }
}