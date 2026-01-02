namespace Core.Entities
{
    public class Speed
    {
        private float _currentSpeed;
        private float _maxSpeed;

        public Speed(float maxSpeed)
        {
            _currentSpeed = 0;
            _maxSpeed = maxSpeed;
        }

        public float GetCurrentSpeed()
        {
            return _currentSpeed;
        }

        public float GetMaxSpeed()
        {
            return _maxSpeed;
        }
        
        public void IncreaseCurrentSpeed(float amount)
        {
            _currentSpeed += amount;
        }

        public void DecreaseCurrentSpeed(float amount)
        {
            _currentSpeed -= amount;
        }
    }
}