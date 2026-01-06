using Core.Entities.Player.Movement;
using UnityEngine;
using Zenject;

namespace Core.Entities
{
    public class PlayerInputController : MonoBehaviour
    {
    	private PlayerMover _playerMover;

        [Inject]
        private void Construct(PlayerMover playerMover)
        {
            _playerMover = playerMover;
        }
        
		private void Update()
        {
            var isMoving = Input.GetKey(KeyCode.W) || 
                           Input.GetKey(KeyCode.A) || 
                           Input.GetKey(KeyCode.S) || 
                           Input.GetKey(KeyCode.D);
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                _playerMover.MoveForward();
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                _playerMover.StopMoveForward(isMoving);
            }
            
			if (Input.GetKeyDown(KeyCode.A))
            {
                _playerMover.MoveLeft();
            }
                
            if (Input.GetKeyUp(KeyCode.A))
            {
                _playerMover.StopMoveLeft(isMoving);
            }
            
			if (Input.GetKeyDown(KeyCode.S))
            {
                _playerMover.MoveBackward();
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                _playerMover.StopMoveBackward(isMoving);
            }
            
			if (Input.GetKeyDown(KeyCode.D))
            {
                _playerMover.MoveRight();
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                _playerMover.StopMoveRight(isMoving);
            }
            
        }
    }
}
