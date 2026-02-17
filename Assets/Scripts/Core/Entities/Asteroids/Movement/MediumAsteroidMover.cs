using Core.Configs;
using Core.World;

namespace Core.Entities.Asteroids.Movement
{
    public class MediumAsteroidMover : LargeAsteroidMover
    {
        public MediumAsteroidMover(AsteroidsData asteroidsData, WorldBoundsChecker worldBoundsChecker) 
            : base(asteroidsData, worldBoundsChecker)
        {
            MovingSpeedX = asteroidsData.MediumAsteroidMovingSpeedX;
            MovingSpeedY = asteroidsData.MediumAsteroidMovingSpeedY;
            RotationSpeed = asteroidsData.MediumAsteroidRotationSpeed;
        }
    }
}