using Core.Configs;
using Core.ObjectMovers;
using Core.PlayerPresentation;
using Core.SpriteControllers;
using Core.WeaponsLogic;
using UnityEngine;
using Zenject;

namespace Core.PlayerLogic
{
    public class PlayerInputController : MonoBehaviour
    {
        private PlayerObject _playerObject;
        private PlayerMover _playerMover;
        private PlayerSpriteController _playerSpriteController;
        private Blasters _blasters;
        private LaserWeapon _laserWeapon;
        private float _movementInput;
        private float _rotationInput;
    
        [Inject]
        private void Construct(
            PlayerMover playerMover, 
            PlayerObject playerObject,
            PlayerSpriteController playerSpriteController,
            Blasters blasters,
            LaserWeapon laserWeapon)
        {
            _playerMover = playerMover;
            _playerObject = playerObject;
            _playerSpriteController = playerSpriteController;
            _blasters = blasters;
            _laserWeapon = laserWeapon;
        }
        
        private void Update()
        {
            if (!_playerObject.isInputEnabled) return;
            
            _movementInput = Input.GetAxis(AxisNames.Vertical);
            _rotationInput = Input.GetAxis(AxisNames.Horizontal);
            
            _playerSpriteController?.UpdatePlayerSprite(_playerObject.isInputEnabled);
            
            if (Input.GetMouseButton(0))
            {
                _blasters.Shoot();
            }

            if (Input.GetMouseButtonDown(1))
            {
                _laserWeapon.Shoot();
            }
        }

        private void FixedUpdate()
        {
            _playerMover?.CalculateVelocity(_movementInput);
            _playerMover?.CalculateRotating(_rotationInput);
        }
        
    }
}
