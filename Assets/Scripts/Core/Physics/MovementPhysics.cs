using UnityEngine;

namespace Core.Physics
{
    public static class MovementPhysics
    {
        public static Vector2 CalculateVelocity(
            Vector2 currentVelocity, 
            Vector2 inputDirection,
            float acceleration,
            float deceleration,
            float maxSpeed,
            float deltaTime)
        {
            if (inputDirection.sqrMagnitude <= 0f)
            {
                return Vector2.MoveTowards(currentVelocity, Vector2.zero, deceleration * deltaTime);    
            }
            var targetVelocity = inputDirection.normalized * maxSpeed ;
            return Vector2.MoveTowards(currentVelocity, targetVelocity, acceleration * deltaTime);
        }
        
        public static Vector2 CalculatePosition(Vector2 position, Vector2 velocity, float deltaTime)
        {
            return position + velocity * deltaTime;
        }
        
    }
}