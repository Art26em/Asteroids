using UnityEngine;

namespace Core.ObjectMovers
{
    public class LargeAsteroidMover : AsteroidMover
    {
        public void StartObjectMoving(GameObject gameObject)
        {
            _ = Move(gameObject);    
        }
    }
}