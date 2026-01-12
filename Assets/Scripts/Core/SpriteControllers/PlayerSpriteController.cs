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

        public void UpdatePlayerSprite(Vector2 inputDirection)
        {
            if (inputDirection.y != 0)
            {
                SetPlayerMovingSprite();
            }

            if (inputDirection is { y: 0, x: 0 })
            {
                SetPlayerIdleSprite();
            }
           
            switch (inputDirection.x)
            {
                case > 0:
                    SetPlayerRollRightSprite();
                    break;
                case < 0:
                    SetPlayerRollLeftSprite();
                    break;
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

        public void SetPlayerRollLeftSprite()
        {
            _playerSpriteRenderer.sprite = _playerRollLeftSprite;
        }

        public void SetPlayerRollRightSprite()
        {
            _playerSpriteRenderer.sprite = _playerRollRightSprite;
        }
    }
}