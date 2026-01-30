namespace Core.Entities.Health
{
    public class HealthStats
    {
        private int _maxHealth = 3;
        private int _currentHealth = 3;
        
        public float GetCurrentHealth()
        {
            return _currentHealth;
        }

        public void SetMaxHealth(int value)
        {
            _maxHealth = value;
        }
        
        public float GetMaxHealth()
        {
            return _maxHealth;
        }
        
        public void IncreaseHealth(int amount)
        {
            _currentHealth += amount;
        }

        public void DecreaseHealth(int amount)
        {
            _currentHealth -= amount;
        }
    }
}