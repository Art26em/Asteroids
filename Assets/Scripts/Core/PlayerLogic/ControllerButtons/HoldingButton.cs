using Core.PlayerPresentation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Core.PlayerLogic.ControllerButtons
{
    [RequireComponent(typeof(Button))]
    public class HoldingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private PlayerObject _playerObject;
        protected bool IsHolding;

        [Inject]
        public void Construct(PlayerObject playerObject)
        {
            _playerObject = playerObject;;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            IsHolding = _playerObject.isInputEnabled;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsHolding = false;
        }
    }
}