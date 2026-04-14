using Core.SpeedSystem;
using UnityEngine;

namespace Core.Physics
{
    public static class CollisionPhysics
    {
        public static Vector2 GetBounceDirection(SpeedStats speedStats, Collision2D collision)
        {
            var normal = collision.contacts[0].normal;
            var incomingDirection = speedStats.CurrentVelocity.normalized;
            var bounceDirection = Vector2.Reflect(incomingDirection, normal).normalized;
            return bounceDirection;
        }
    }
}