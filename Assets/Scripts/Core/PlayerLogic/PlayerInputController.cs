using Core.ObjectMovers;
using Core.SpriteControllers;
using Core.WeaponsLogic;
using UnityEngine;
using Zenject;

namespace Core.PlayerLogic
{
    public class PlayerInputController : MonoBehaviour
    {
    	private PlayerMover _playerMover;
        private PlayerSpriteController _playerSpriteController;
        private Blasters _blasters;
        private LaserWeapon _laserWeapon;
    
        [Inject]
        private void Construct(
            PlayerMover playerMover, 
            PlayerSpriteController playerSpriteController,
            Blasters blasters,
            LaserWeapon laserWeapon)
        {
            _playerMover = playerMover;
            _playerSpriteController = playerSpriteController;
            _blasters = blasters;
            _laserWeapon = laserWeapon;
        }
        
        private void Update()
        {
            _playerMover?.CalculateVelocity();
            _playerSpriteController?.UpdatePlayerSprite();
    
            if (Input.GetMouseButtonDown(0))
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
            _playerMover?.CalculatePosition(); 
            _playerMover?.CalculateRotating();
        }
        
    }
}
