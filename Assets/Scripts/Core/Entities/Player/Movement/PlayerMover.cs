using Core.Entities.Physics;
using Core.SpriteControllers;
using UnityEngine;
using Zenject;

namespace Core.Entities.Player.Movement
{
    public class PlayerMover
    { 
       private readonly GameObject _playerObject;
       private readonly PlayerSpriteController _playerSpriteController;
       private SpriteRenderer _playerSpriteRenderer;
       private MovementState _movementState;
       private readonly float _acceleration;
       private readonly float _deceleration;
       private readonly float _maxSpeed;
       
       public PlayerMover(
           GameObject playerObject,
           PlayerSpriteController playerSpriteController,
           Vector2 startPosition,
           PlayerStats playerStats)
       {
           _playerObject = playerObject;
           _playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();
           _playerSpriteController = playerSpriteController;
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
           UpdatePlayerSprite(inputDirection);
           
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

       private void UpdatePlayerSprite(Vector2 inputDirection)
       {
           switch (inputDirection.x)
           {
               case > 0:
                   _playerSpriteController.SetPlayerRollRightSprite(ref _playerSpriteRenderer);
                   break;
               case < 0:
                   _playerSpriteController.SetPlayerRollLeftSprite(ref _playerSpriteRenderer);
                   break;
           }

           if (inputDirection.y != 0)
           {
               _playerSpriteController.SetPlayerMovingSprite(ref _playerSpriteRenderer);
           }

           if (inputDirection is { y: 0, x: 0 })
           {
               SetDefaultPlayerSprite();
           }
       }

       public void SetDefaultPlayerSprite()
       {
           _playerSpriteController.SetPlayerIdleSprite(ref _playerSpriteRenderer);
       }
    }
}
