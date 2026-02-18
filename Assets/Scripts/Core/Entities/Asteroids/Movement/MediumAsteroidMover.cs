using Core.Configs;
using Core.ObjectMovers;
using Core.World;

namespace Core.Entities.Asteroids.Movement
{
    public class MediumAsteroidMover : LargeAsteroidMover, IMover<MediumAsteroid>
    {
        public MediumAsteroidMover(AsteroidsData asteroidsData, WorldBoundsChecker worldBoundsChecker) 
            : base(asteroidsData, worldBoundsChecker)
        {
            MovingSpeedX = asteroidsData.MediumAsteroidMovingSpeedX;
            MovingSpeedY = asteroidsData.MediumAsteroidMovingSpeedY;
            RotationSpeed = asteroidsData.MediumAsteroidRotationSpeed;
        }

        public void StartObjectMoving(MediumAsteroid asteroid)
        {
            _ = Move(asteroid.gameObject); 
        }
    }
}