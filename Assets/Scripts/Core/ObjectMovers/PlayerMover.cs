using Core.Configs;
using Core.Entities.Physics;
using Core.Physics;
using Core.PlayerPresentation;
using UnityEngine;

namespace Core.ObjectMovers
{
    public class PlayerMover
    { 
       private readonly PlayerObject _playerObject;
       private readonly PlayerStats _playerStats;
       private readonly float _acceleration;
       private readonly float _deceleration;
       private readonly float _maxSpeed;
       private readonly float _rotationSpeed;
       
       public PlayerMover(PlayerObject playerObject, PlayerStats playerStats)
       {
           _playerStats = playerStats;
           _playerObject = playerObject;
           _acceleration = playerStats.SpeedStats.Acceleration;
           _deceleration = playerStats.SpeedStats.Deceleration;
           _maxSpeed = playerStats.SpeedStats.MaxSpeed;
           _rotationSpeed = playerStats.SpeedStats.RotationSpeed;
       }

       public void HandleMoving()
       {
           var inputDirection = Input.GetAxis(AxisNames.Vertical) * _playerObject.transform.up; 
           
            _playerStats.SpeedStats.CurrentVelocity = MovementPhysics.CalculateVelocity(
                _playerStats.SpeedStats.CurrentVelocity,
                inputDirection,
                _acceleration,
                _deceleration,
                _maxSpeed,
                Time.deltaTime);

            _playerObject.transform.position = MovementPhysics.CalculatePosition(
                _playerObject.transform.position,
                _playerStats.SpeedStats.CurrentVelocity,
                Time.deltaTime);
           
        }
       
        public void HandleRotating()
        {
            var angle = -Input.GetAxis(AxisNames.Horizontal);
            _playerObject.transform.Rotate(Vector3.forward,  angle * Time.deltaTime * _rotationSpeed);    
        }
       
    }
}
