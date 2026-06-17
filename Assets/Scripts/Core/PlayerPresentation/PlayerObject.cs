using Core.EffectsControllers;
using UnityEngine;
using Zenject;

namespace Core.PlayerPresentation
{
    public class PlayerObject : MonoBehaviour
    {
        public bool IsInputEnabled { get; set; } = true;

        private EffectsController _effectsController;

        [Inject]
        private void Construct(EffectsController effectsController)
        {
            _effectsController = effectsController;    
        }
        
        public void PlayInvulnerabilityEffect()
        {
            _effectsController.PlayerInvulnerabilityEffect.Play();
            IsInputEnabled = false;
        }

        public void StopInvulnerabilityEffect()
        {
            _effectsController.PlayerInvulnerabilityEffect.Stop();
            IsInputEnabled = true;
        }
        
    }
}