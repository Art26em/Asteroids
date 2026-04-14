using System;
using Core.SpeedSystem;
using UnityEngine;

namespace Core.Physics
{
    public static class MovementPhysics
    {
        public static Vector2 GetNewPosition(Vector2 position, SpeedStats speedStats)
        {
            return position + speedStats.CurrentVelocity * Time.deltaTime;
        }
        
        public static Vector2 GetNewAcceleratedVelocity(Vector2 inputDirection, SpeedStats speedStats)
        {
            Vector2 newVelocity;
            if (inputDirection.sqrMagnitude > 0f)
            {
                newVelocity = speedStats.CurrentVelocity + inputDirection * (speedStats.Acceleration * Time.deltaTime);
                if (newVelocity.magnitude > speedStats.MaxSpeed)
                {
                    newVelocity = newVelocity.normalized * speedStats.MaxSpeed;       
                }
            }
            else
            {
                var newSpeed = speedStats.CurrentSpeed - speedStats.Deceleration * Time.deltaTime;
                newSpeed = Math.Max(0, newSpeed);
                newVelocity = speedStats.CurrentDirection * newSpeed;
            }
            return newVelocity;
        }

        public static Vector2 GetNewSeekingVelocity(
            Vector2 currentPosition, 
            Vector2 targetPosition, 
            SpeedStats speedStats)
        {
            return Vector2.MoveTowards(speedStats.CurrentVelocity,
                (targetPosition - currentPosition).normalized * speedStats.MaxSpeed, 
                speedStats.Acceleration * Time.deltaTime);
        }
        
    }
}