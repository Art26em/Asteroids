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
    
        [Inject]
        private void Construct(
            PlayerMover playerMover, 
            PlayerSpriteController playerSpriteController,
            Blasters blasters)
        {
            _playerMover = playerMover;
            _playerSpriteController = playerSpriteController;
            _blasters = blasters;
        }
        
        private void Update()
        {
            _playerMover?.HandleMoving();
            _playerSpriteController?.UpdatePlayerSprite();
            _playerMover?.HandleRotating();
    
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _blasters.Shoot();
            }
            
        }
    }
}
