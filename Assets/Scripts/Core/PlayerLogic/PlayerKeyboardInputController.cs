using Core.Configs;
using Core.PlayerPresentation;
using UnityEngine;
using Zenject;

namespace Core.PlayerLogic
{
    public class PlayerKeyboardInputController : IPlayerInputProvider
    {
        private readonly PlayerInputData _playerInputData = new();
        private PlayerObject _playerObject;

        [Inject]
        public void Construct(PlayerObject playerObject)
        {
            _playerObject = playerObject;
        }

    public PlayerInputData GetPlayerInput()
        {
            _playerInputData.MovementInput = 
                _playerObject.IsInputEnabled ? Input.GetAxis(AxisNames.Vertical) : 0;
            
            _playerInputData.RotationInput = 
                _playerObject.IsInputEnabled ? Input.GetAxis(AxisNames.Horizontal) : 0;
            
            _playerInputData.IsMovingY = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || 
                                         Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                                         && _playerObject.IsInputEnabled;

            _playerInputData.IsBlastersShooting = Input.GetMouseButton(0) && _playerObject.IsInputEnabled;
            _playerInputData.IsLaserShooting = Input.GetMouseButton(1) && _playerObject.IsInputEnabled;

            return _playerInputData;
        }
    }
}
