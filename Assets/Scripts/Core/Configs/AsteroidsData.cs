namespace Core.Configs
{
    public class AsteroidsData
    {
        public readonly float MovingSpeedX;
        public readonly float MovingSpeedY;
        public readonly float RotationSpeed;
        public readonly float TimeToSpawn;

        public AsteroidsData()
        {
            MovingSpeedX = 0.1f;
            MovingSpeedY = -0.1f;
            RotationSpeed = 0.2f;
            TimeToSpawn = 5;    
        }
        
    }
}