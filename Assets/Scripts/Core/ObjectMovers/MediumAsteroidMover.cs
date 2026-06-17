using UnityEngine;

namespace Core.ObjectMovers
{
    public class MediumAsteroidMover : AsteroidMover
    {
        public void StartObjectMoving(GameObject gameObject)
        {
            _ = Move(gameObject);    
        }
    }
}