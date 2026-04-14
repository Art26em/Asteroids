using Core.Configs;
using Core.Physics;
using Core.PlayerPresentation;
using UnityEngine;
using Zenject;

namespace Core.ObjectMovers
{
    public class PlayerMover
    { 
       private PlayerStats _playerStats;
       private Rigidbody2D _playerRigidbody;

       [Inject]
       private void Construct(PlayerObject playerObject, PlayerStats playerStats)
       {
           _playerStats = playerStats;
           _playerRigidbody = playerObject.GetComponent<Rigidbody2D>();
       }
       
       public void CalculateVelocity()
       {
           var inputDirection = Input.GetAxis(AxisNames.Vertical) * _playerRigidbody.transform.up; 
           
           _playerStats.SpeedStats.CurrentVelocity = MovementPhysics.GetNewAcceleratedVelocity(
               inputDirection, 
               _playerStats.SpeedStats);
        }
       
        public void CalculatePosition()
        {
            var newPosition = MovementPhysics.GetNewPosition(
                _playerRigidbody.position, 
                _playerStats.SpeedStats);
            _playerRigidbody.MovePosition(newPosition);    
        }
        
        public void CalculateRotating()
        {
            var rotationChange = -Input.GetAxis(AxisNames.Horizontal) * 
                                      _playerStats.SpeedStats.RotationSpeed * 
                                      Time.fixedDeltaTime;
            var angle = Quaternion.Euler(0, 0, rotationChange);
            Quaternion newRotation = _playerRigidbody.gameObject.transform.rotation * angle;
            _playerRigidbody.MoveRotation(newRotation);    
        }
        
    }
}
