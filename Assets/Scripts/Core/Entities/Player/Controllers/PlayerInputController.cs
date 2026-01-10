using Core.Entities.Player.Controllers;
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
            var input = GetMovementInput();
            if (input.sqrMagnitude > 0)
            {
                _playerMover.Move(input, Time.deltaTime);    
            }
        }

        private Vector2 GetMovementInput()
        {
            return new Vector2(Input.GetAxis(AxisNames.Horizontal), Input.GetAxis(AxisNames.Vertical));
        }
        
    }
}
