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
       
       public void CalculateVelocity(float input)
       {
           var inputDirection = input * _playerRigidbody.transform.up;
           
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
           _playerStats.CurrentPosition = newPosition;
       }
        
       public void CalculateRotating(float input)
       {
           var rotationChange = -input * 
                                _playerStats.SpeedStats.RotationSpeed * 
                                Time.fixedDeltaTime;
           var angle = Quaternion.Euler(0, 0, rotationChange);
           Quaternion newRotation = _playerRigidbody.gameObject.transform.rotation * angle;
            
           _playerRigidbody.MoveRotation(newRotation);  
           _playerStats.SpeedStats.CurrentRotation = newRotation.eulerAngles;
       }
        
    }
}
