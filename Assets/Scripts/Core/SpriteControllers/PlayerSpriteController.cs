using Core.Entities.Player.Controllers;
using UnityEngine;

namespace Core.SpriteControllers
{
    public class PlayerSpriteController
    {
        private readonly Sprite _playerIdleSprite;
        private readonly Sprite _playerMovingSprite;
        private readonly Sprite _playerRollLeftSprite;
        private readonly Sprite _playerRollRightSprite;
        private readonly SpriteRenderer _playerSpriteRenderer;

        public PlayerSpriteController(
            Sprite playerIdleSprite, 
            Sprite playerMovingSprite, 
            Sprite playerRollLeftSprite, 
            Sprite playerRollRightSprite,
            SpriteRenderer playerSpriteRenderer)
        {
            _playerIdleSprite = playerIdleSprite;
            _playerMovingSprite = playerMovingSprite;
            _playerRollLeftSprite = playerRollLeftSprite;
            _playerRollRightSprite = playerRollRightSprite;
            _playerSpriteRenderer = playerSpriteRenderer;
        }

        public void UpdatePlayerSprite()
        {
            var isMovingY = Input.GetAxis(AxisNames.Vertical) != 0;
            var isMovingLeft = Input.GetAxis(AxisNames.Horizontal) < 0;
            var isMovingRight = Input.GetAxis(AxisNames.Horizontal) > 0;;
            
            if (isMovingY && _playerSpriteRenderer.sprite != _playerMovingSprite)
            {
                SetPlayerMovingSprite();
            }

            if (isMovingLeft && _playerSpriteRenderer.sprite != _playerRollLeftSprite)
            {
                SetPlayerRollLeftSprite();    
            }    
                
            if (isMovingRight && _playerSpriteRenderer.sprite != _playerRollRightSprite)
            {
                SetPlayerRollRightSprite();    
            } 
            
            if (!isMovingY && !isMovingLeft &&!isMovingRight && _playerSpriteRenderer.sprite != _playerIdleSprite)
            {
                SetPlayerIdleSprite();
            }
        }

        public void SetPlayerIdleSprite()
        {
            _playerSpriteRenderer.sprite = _playerIdleSprite;
        }

        public void SetPlayerMovingSprite()
        {
            _playerSpriteRenderer.sprite = _playerMovingSprite;
        }

        private void SetPlayerRollLeftSprite()
        {
            _playerSpriteRenderer.sprite = _playerRollLeftSprite;
        }

        private void SetPlayerRollRightSprite()
        {
            _playerSpriteRenderer.sprite = _playerRollRightSprite;
        }
    }
}