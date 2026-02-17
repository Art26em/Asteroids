using UnityEngine;

namespace Core.Entities.Player.Fighting.Projectiles
{
    public class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerObject _)) return;
            gameObject.SetActive(false);
        }
    }
}