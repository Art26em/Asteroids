namespace Core.HealthSystem

{
    public class HealthStats
    {
        public int MaxHealth;
        public int CurrentHealth;
        
        public void SetMaxHealth(int value)
        {
            if (value <= 0) return;
            MaxHealth = value;
        }
        
        public void IncreaseHealth(int amount = 1)
        {
            if (CurrentHealth + amount > MaxHealth) return;
            CurrentHealth += amount;
        }
    
        public void DecreaseHealth(int amount = 1)
        {
            CurrentHealth -= amount;
        }
    }
}