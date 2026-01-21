using UnityEngine;

namespace Core.World
{
    public class WorldBoundsChecker
    {
        private readonly Camera _camera = Camera.main;
        
        public Vector3 CheckPlayerWorldPosition(Vector3 playerWorldPosition)
        {
            var viewportPosition = _camera.WorldToViewportPoint(playerWorldPosition);
            if (viewportPosition.x < 0)
            {
                viewportPosition.x = 1;
                playerWorldPosition = _camera.ViewportToWorldPoint(viewportPosition);
            }
           
            if (viewportPosition.x > 1)
            {
                viewportPosition.x = 0;
                playerWorldPosition = _camera.ViewportToWorldPoint(viewportPosition);
            }
              
            if (viewportPosition.y < 0)
            {
                viewportPosition.y = 1;
                playerWorldPosition = _camera.ViewportToWorldPoint(viewportPosition);
            } 
            if (viewportPosition.y > 1)
            {
                viewportPosition.y = 0;
                playerWorldPosition = _camera.ViewportToWorldPoint(viewportPosition);
            }
            
            return playerWorldPosition;
        }
        
    }
    
    
    
}