using Core.Entities.Player.Movement;
using Core.SpriteControllers;
using Core.World;
using UnityEngine;
using Zenject;

namespace Core.Entities.Player.Controllers
{
    public class PlayerInputController : MonoBehaviour
    {
    	private PlayerMover _playerMover;
        private PlayerSpriteController _playerSpriteController;
        private WorldBoundsChecker _worldBoundsChecker;

        [Inject]
        private void Construct(
            PlayerMover playerMover, 
            PlayerSpriteController playerSpriteController,
            WorldBoundsChecker worldBoundsChecker)
        {
            _playerMover = playerMover;
            _playerSpriteController = playerSpriteController;
            _worldBoundsChecker = worldBoundsChecker;
        }
        
        private void Update()
        {
            _playerMover?.Move(GetMovementInput(), Time.deltaTime); 
            _playerSpriteController?.UpdatePlayerSprite();
            transform.position = _worldBoundsChecker.CheckPlayerWorldPosition(transform.position);
        }
        
        private Vector2 GetMovementInput()
        {
            return new Vector2(Input.GetAxis(AxisNames.Horizontal), Input.GetAxis(AxisNames.Vertical));
        }

    }
}
