using Core.Entities.Player.Movement;
using Core.SpriteControllers;
using UnityEngine;
using Zenject;

namespace Core.Entities.Player.Controllers
{
    public class PlayerInputController : MonoBehaviour
    {
    	private PlayerMover _playerMover;
        private PlayerSpriteController _playerSpriteController;

        [Inject]
        private void Construct(PlayerMover playerMover, PlayerSpriteController playerSpriteController)
        {
            _playerMover = playerMover;
            _playerSpriteController = playerSpriteController;
        }
        
		private void Update()
        {
            _playerMover?.Move(GetMovementInput(), Time.deltaTime); 
            _playerSpriteController?.UpdatePlayerSprite();
        }
        
        private Vector2 GetMovementInput()
        {
            return new Vector2(Input.GetAxis(AxisNames.Horizontal), Input.GetAxis(AxisNames.Vertical));
        }

    }
}
