using Core.Entities.Physics;
using Core.Entities.Player.Controllers;
using Core.World;
using UnityEngine;

namespace Core.Entities.Player.Movement
{
    public class PlayerMover
    { 
       private readonly GameObject _playerObject;
       private readonly PlayerStats _playerStats;
       private readonly WorldBoundsChecker _worldBoundsChecker;
       private readonly float _acceleration;
       private readonly float _deceleration;
       private readonly float _maxSpeed;
       private readonly float _rotationSpeed;
       
       public PlayerMover(
           GameObject playerObject,
           PlayerStats playerStats,
           WorldBoundsChecker worldBoundsChecker)
       {
           _playerStats = playerStats;
           _playerObject = playerObject;
           _acceleration = playerStats.SpeedStats.Acceleration;
           _deceleration = playerStats.SpeedStats.Deceleration;
           _maxSpeed = playerStats.SpeedStats.MaxSpeed;
           _rotationSpeed = playerStats.SpeedStats.RotationSpeed;
           _worldBoundsChecker = worldBoundsChecker;
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
           
           _playerObject.transform.position = _worldBoundsChecker.GetObjectWorldPosition(
               _playerObject.transform.position);
       }

       public void HandleRotating()
       {
           var angle = -Input.GetAxis(AxisNames.Horizontal);
           _playerObject.transform.Rotate(Vector3.forward,  angle * Time.deltaTime * _rotationSpeed);    
       }
       
    }
}
