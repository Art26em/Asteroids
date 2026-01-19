using Core.Entities.Physics;
using UnityEngine;

namespace Core.Entities.Player.Movement
{
    public class PlayerMover
    { 
       private readonly GameObject _playerObject;
       private readonly PlayerStats _playerStats;
       private readonly float _acceleration;
       private readonly float _deceleration;
       private readonly float _maxSpeed;
       
       public PlayerMover(
           GameObject playerObject,
           PlayerStats playerStats)
       {
           _playerStats = playerStats;
           _playerObject = playerObject;
           _acceleration = playerStats.SpeedStats.Acceleration;
           _deceleration = playerStats.SpeedStats.Deceleration;
           _maxSpeed = playerStats.SpeedStats.MaxSpeed;
       }

       public void Move(Vector2 inputDirection, float deltaTime)
       {
           _playerStats.SpeedStats.CurrentVelocity = MovementPhysics.CalculateVelocity(
               _playerStats.SpeedStats.CurrentVelocity,
               inputDirection,
               _acceleration,
               _deceleration,
               _maxSpeed,
               deltaTime);

           _playerObject.transform.position = MovementPhysics.CalculatePosition(
               _playerObject.transform.position,
               _playerStats.SpeedStats.CurrentVelocity,
               deltaTime);
           
       }
       
    }
}
