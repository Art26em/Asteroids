using Core.PlayerPresentation;
using Core.SpriteControllers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Core.PlayerLogic
{
    [RequireComponent(typeof(Button))]
    public class MobileInputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private ControlAction _controlAction;
        
        private PlayerInputData _playerInputData;
        private PlayerObject _playerObject;
        private PlayerSpriteController _playerSpriteController;
        private bool _isHolding;
        
        [Inject]
        public void Construct(
            PlayerObject playerObject, 
            PlayerInputData playerInputData,  
            PlayerSpriteController playerSpriteController)
        {
            _playerObject = playerObject;
            _playerInputData = playerInputData;
            _playerSpriteController = playerSpriteController;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isHolding = _playerObject.IsInputEnabled;
            switch (_controlAction)
            {
                case ControlAction.MoveForward:
                    _playerInputData.MovementInput = _isHolding ? 1 : 0;
                    _playerSpriteController.SetPlayerMovingSprite();
                    break;
                case ControlAction.MoveBackward:
                    _playerInputData.MovementInput = _isHolding ? -1 : 0;
                    break;
                case ControlAction.RotateRight:
                    _playerInputData.RotationInput = _isHolding ? 1 : 0;
                    break;
                case ControlAction.RotateLeft:
                    _playerInputData.RotationInput = _isHolding ? -1 : 0;
                    break;
                case ControlAction.BlastersWeaponShoot:
                    _playerInputData.IsBlastersShooting = _isHolding;
                    break;
                case ControlAction.LaserWeaponShoot:
                    _playerInputData.IsLaserShooting = _isHolding;
                    break;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            switch (_controlAction)
            {
                case ControlAction.MoveForward:
                    _playerInputData.MovementInput = 0;
                    break;
                case ControlAction.MoveBackward:
                    _playerInputData.MovementInput = 0;
                    break;
                case ControlAction.RotateRight:
                    _playerInputData.RotationInput = 0;
                    break;
                case ControlAction.RotateLeft:
                    _playerInputData.RotationInput = 0;
                    break;
                case ControlAction.BlastersWeaponShoot:
                    _playerInputData.IsBlastersShooting = false;
                    break;
                case ControlAction.LaserWeaponShoot:
                    _playerInputData.IsLaserShooting = false;
                    break;
            }
            _playerSpriteController.SetPlayerIdleSprite();
        }
        
        private void Update()
        {
            
        }
        
    }
}