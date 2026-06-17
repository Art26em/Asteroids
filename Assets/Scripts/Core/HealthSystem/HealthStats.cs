using System;

namespace Core.HealthSystem

{
    public class HealthStats
    {
        public int MaxHealth;
        public int CurrentHealth;
        public event Action HealthChanged;
        
        public void DecreaseHealth(int amount = 1)
        {
            if (IsDead()) return;
            CurrentHealth -= amount;
            HealthChanged?.Invoke();
        }

        public bool IsDead()
        {
            return CurrentHealth <= 0;
        }
        
    }
}