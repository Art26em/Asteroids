using Core.Entities.Physics;
using Core.SpriteControllers;
using UnityEngine;
using Zenject;

namespace Core.Entities.Player.Movement
{
    public class PlayerMover
    { 
       private readonly GameObject _playerObject;
       private MovementState _movementState;
       private readonly float _acceleration;
       private readonly float _deceleration;
       private readonly float _maxSpeed;
       
       public PlayerMover(
           GameObject playerObject,
           Vector2 startPosition,
           PlayerStats playerStats)
       {
           _playerObject = playerObject;
           _movementState = new MovementState()
           {
               Position = startPosition,
               Velocity = Vector2.zero,    
           };
           
           _acceleration = playerStats.SpeedStats.Acceleration;
           _deceleration = playerStats.SpeedStats.Deceleration;
           _maxSpeed = playerStats.SpeedStats.MaxSpeed;
       }

       public void Move(Vector2 inputDirection, float deltaTime)
       {
           _movementState.Velocity = MovementPhysics.CalculateVelocity(
               _movementState.Velocity,
               inputDirection,
               _acceleration,
               _deceleration,
               _maxSpeed,
               deltaTime);

           _movementState.Position = MovementPhysics.CalculatePosition(
               _movementState.Position,
               _movementState.Velocity,
               deltaTime);
           
           _playerObject.transform.position = _movementState.Position;
           
       }
       
    }
}
