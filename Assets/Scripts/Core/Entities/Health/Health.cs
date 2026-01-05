namespace Core.Entities
{
    public class Health
    {
        private int _currentHealth;
        private int _maxHealth;

        public Health(int maxHealth)
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