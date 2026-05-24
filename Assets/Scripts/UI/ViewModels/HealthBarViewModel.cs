using System;
using Core.PlayerPresentation;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class HealthBarViewModel : IInitializable, IDisposable
    {
        [Data("HealthBar")]
        public readonly ReactiveCollection<HealthItemViewModel> Collection = new();
        
        private PlayerStats _playerStats;

        [Inject]
        private void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        public void Initialize()
        {
            _playerStats.HealthChanged += OnHealthChanged;

            var currentHealth = _playerStats.CurrentHealth();
            for (var i = 0; i < currentHealth; i++)
            {
                var model = new HealthItemViewModel();
                Collection.Add(model);
            }
        }

        public void Dispose()
        {
            _playerStats.HealthChanged -= OnHealthChanged;
            
            for (var i = 0; i < Collection.Count; i++)
            {
                var model = Collection[i];
                Collection.Remove(model);
            }
        }

        private void OnHealthChanged(int currentHealth)
        {
            if (currentHealth < Collection.Count && currentHealth >= 0)
            {
                var model = Collection[^1];
                Collection.Remove(model);
            }
            else
            {
                var model = new HealthItemViewModel();
                Collection.Add(model);      
            }
        }
    }
}