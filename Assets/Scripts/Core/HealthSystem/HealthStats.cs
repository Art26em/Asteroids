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
            CurrentHealth += amount;
            CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        }
    
        public void DecreaseHealth(int amount = 1)
        {
            CurrentHealth -= amount;
        }

        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }
        
    }
}