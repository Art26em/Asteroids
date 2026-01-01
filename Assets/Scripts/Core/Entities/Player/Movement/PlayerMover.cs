using Core.SpriteControllers;
using UnityEngine;

namespace Core.Entities.Player.Movement
{
    public class PlayerMover
    {
       private readonly PlayerSpriteController _playerSpriteController;
       private Transform _playerTransform;
       private SpriteRenderer _playerSpriteRenderer;
       
       public PlayerMover(GameObject player, PlayerSpriteController playerSpriteController)
       {
           _playerTransform = player.GetComponent<Transform>();
           _playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
           _playerSpriteController = playerSpriteController;
       }

       public void MoveForward()
       {
           _playerSpriteController.SetPlayerMovingSprite(ref _playerSpriteRenderer);
       }
       
       public void StopMoveForward()
       {
           _playerSpriteController.SetPlayerIdleSprite(ref _playerSpriteRenderer);
       }
       
       public void MoveBackward()
       {
           _playerSpriteController.SetPlayerMovingSprite(ref _playerSpriteRenderer);    
       }
       
       public void StopMoveBackward()
       {
           _playerSpriteController.SetPlayerIdleSprite(ref _playerSpriteRenderer);    
       }
       
       public void MoveLeft()
       {
           _playerSpriteController.SetPlayerRollLeftSprite(ref _playerSpriteRenderer);    
       }
       
       public void StopMoveLeft()
       {
           _playerSpriteController.SetPlayerIdleSprite(ref _playerSpriteRenderer);    
       }
       
       public void MoveRight()
       {
           _playerSpriteController.SetPlayerRollRightSprite(ref _playerSpriteRenderer);    
       }
       
       public void StopMoveRight()
       {
           _playerSpriteController.SetPlayerIdleSprite(ref _playerSpriteRenderer);    
       }
       
    }
}
