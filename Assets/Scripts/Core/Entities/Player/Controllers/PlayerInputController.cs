using Core.Entities.Player.Controllers;
using Core.Entities.Player.Movement;
using Core.SpriteControllers;
using UnityEngine;
using Zenject;

namespace Core.Entities
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
            var input = GetMovementInput();
            _playerMover?.Move(input, Time.deltaTime);
            _playerSpriteController?.UpdatePlayerSprite(input);
        }

        private Vector2 GetMovementInput()
        {
            return new Vector2(Input.GetAxis(AxisNames.Horizontal), Input.GetAxis(AxisNames.Vertical));
        }
        
    }
}
