using System;

namespace Core.HealthSystem

{
    public class HealthStats
    {
        public int MaxHealth;
        public int CurrentHealth;
        public event Action HealthChanged;
        
        public void SetMaxHealth(int value)
        {
            if (value <= 0) return;
            MaxHealth = value;
        }
        
        public void IncreaseHealth(int amount = 1)
        {
            CurrentHealth += amount;
            CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
            HealthChanged?.Invoke();
        }
    
        public void DecreaseHealth(int amount = 1)
        {
            if (!IsDead())
            {
                CurrentHealth -= amount;
                HealthChanged?.Invoke();    
            }
        }

        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }
        
    }
}