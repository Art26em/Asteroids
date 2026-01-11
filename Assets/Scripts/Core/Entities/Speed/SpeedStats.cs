namespace Core.Entities.Speed
{
    public class SpeedStats
    {
        public float CurrentSpeed {get; private set;}
        public float MaxSpeed {get; private set;}
        public float Acceleration {get; private set;}
        public float Deceleration {get; private set;}
        

        public SpeedStats(float maxSpeed,  float acceleration = 1f, float deceleration = 1f)
        {
            CurrentSpeed = 0;
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Deceleration = deceleration;
        }
        
        public void IncreaseCurrentSpeed(float amount)
        {
            CurrentSpeed += amount;
        }

        public void DecreaseCurrentSpeed(float amount)
        {
            CurrentSpeed -= amount;
        }
    }
}