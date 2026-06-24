using Core.EffectsControllers;
using UnityEngine;
using Zenject;

namespace Core.EnemiesPresentation
{
    public abstract class Enemy : MonoBehaviour
    {
        protected EffectsController EffectsController;

        [Inject]
        private void Construct(EffectsController effectsController)
        {
            EffectsController = effectsController;
        }
    }
}