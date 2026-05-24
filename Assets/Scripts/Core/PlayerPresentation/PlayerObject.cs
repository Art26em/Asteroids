using UnityEngine;

namespace Core.PlayerPresentation
{
    public class PlayerObject : MonoBehaviour
    {
        [SerializeField] private ParticleSystem invulnerabilityRing;

        public bool isInputEnabled = true;
        
        public void PlayInvulnerabilityEffect()
        {
            invulnerabilityRing.Play();
            isInputEnabled = false;
        }

        public void StopInvulnerabilityEffect()
        {
            invulnerabilityRing.Stop();
            isInputEnabled = true;
        }
        
    }
}