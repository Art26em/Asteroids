using Core.ObjectPools;
using UnityEngine;

namespace Core.World
{
    public class WorldBoundsChecker
    {
        private readonly Camera _camera = Camera.main;
        
        public Vector3 GetObjectWorldPosition(Vector3 objectWorldPosition)
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

        public void ReturnObjectToPool<T>(Vector3 objectWorldPosition, T item, ObjectPool<T> objectPool) where T : Component
        {
            var viewportPosition = _camera.WorldToViewportPoint(objectWorldPosition);
            
            if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0  || viewportPosition.y > 1)
            {
                viewportPosition.x = 1;
                objectPool.ReturnItemToPool(item);
            }
            
        }
        
    }
    
    
    
}