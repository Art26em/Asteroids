using Core.ProjectilesPresentation;
using UnityEngine;

namespace Core.World
{
    public class WorldBoundsChecker : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                bullet.gameObject.SetActive(false);
            }
            else
            {
                other.transform.position = GetObjectWorldPosition(other.transform.position);         
            }
        }

        private Vector3 GetObjectWorldPosition(Vector3 objectWorldPosition)
        {
            var viewportPosition = _camera.WorldToViewportPoint(objectWorldPosition);
            
            if (viewportPosition.x < 0)
            {
                viewportPosition.x = 1;
                objectWorldPosition = _camera.ViewportToWorldPoint(viewportPosition);
            }
           
            if (viewportPosition.x > 1)
            {
                viewportPosition.x = 0;
                objectWorldPosition = _camera.ViewportToWorldPoint(viewportPosition);
            }
              
            if (viewportPosition.y < 0)
            {
                viewportPosition.y = 1;
                objectWorldPosition = _camera.ViewportToWorldPoint(viewportPosition);
            } 
            
            if (viewportPosition.y > 1)
            {
                viewportPosition.y = 0;
                objectWorldPosition = _camera.ViewportToWorldPoint(viewportPosition);
            }
        
            return objectWorldPosition;
        }
        
    }
    
    
    
}