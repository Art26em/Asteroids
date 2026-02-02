namespace Core.Entities.Health
{
    public class HealthStats
    {
        public int MaxHealth { get; private set; } = 3;
        public int CurrentHealth { get; private set; } = 3;
        
        public void SetMaxHealth(int value)
        {
            if (value <= 0) return;
            MaxHealth = value;
        }
        
        public void IncreaseHealth(int amount)
        {
            CurrentHealth += amount;
        }

        public void DecreaseHealth(int amount)
        {
            CurrentHealth -= amount;
        }
    }
}