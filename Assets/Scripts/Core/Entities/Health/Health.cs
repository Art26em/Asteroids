namespace Core.Entities
{
    public class Health
    {
        private float _currentHealth;
        private float _maxHealth;

        public Health(float maxHealth)
        {
            _currentHealth = maxHealth;
            _maxHealth = maxHealth;
        }

        public float GetCurrentHealth()
        {
            return _currentHealth;
        }

        public float GetMaxHealth()
        {
            return _maxHealth;
        }
        
        public void IncreaseHealth(float amount)
        {
            _currentHealth += amount;
        }

        public void DecreaseHealth(float amount)
        {
            _currentHealth -= amount;
        }
    }
}