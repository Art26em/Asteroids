namespace Core.Entities
{
    public class Health
    {
        private float _health;

        public Health(float health)
        {
            _health = health;
        }

        public float GetHealth()
        {
            return _health;
        }

        public void IncreaseHealth(float amount)
        {
            _health += amount;
        }

        public void DecreaseHealth(float amount)
        {
            _health -= amount;
        }
        
    }
}