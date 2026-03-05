using Core.Configs;
using Core.Entities.Physics;
using Core.Physics;
using Core.PlayerPresentation;
using UnityEngine;
using Zenject;

namespace Core.ObjectMovers
{
    public class PlayerMover
    { 
       private PlayerObject _playerObject;
       private PlayerStats _playerStats;
       private float _acceleration;
       private float _deceleration;
       private float _maxSpeed;
       private float _rotationSpeed;

       [Inject]
       private void Construct(PlayerObject playerObject, PlayerStats playerStats)
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
