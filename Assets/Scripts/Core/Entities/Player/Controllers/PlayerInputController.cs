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
            if (Input.GetKey(KeyCode.W))
            {
                _playerMover.MoveForward();
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                _playerMover.StopMoveForward();
            }
            
			if (Input.GetKey(KeyCode.A))
            {
                _playerMover.MoveLeft();
            }
                
            if (Input.GetKeyUp(KeyCode.A))
            {
                _playerMover.StopMoveLeft();
            }
            
			if (Input.GetKey(KeyCode.S))
            {
                _playerMover.MoveBackward();
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                _playerMover.StopMoveBackward();
            }
            
			if (Input.GetKey(KeyCode.D))
            {
                _playerMover.MoveRight();
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                _playerMover.StopMoveRight();
            }
            
        }
    }
}
