using UnityEngine;

namespace Core.SpriteControllers
{
    public class PlayerSpriteController
    {
        private readonly Sprite _playerIdleSprite;
        private readonly Sprite _playerMovingSprite;
        private readonly Sprite _playerRollLeftSprite;
        private readonly Sprite _playerRollRightSprite;

        public PlayerSpriteController(Sprite playerIdleSprite, Sprite playerMovingSprite, Sprite playerRollLeftSprite, Sprite playerRollRightSprite)
        {
            _playerIdleSprite = playerIdleSprite;
            _playerMovingSprite = playerMovingSprite;
            _playerRollLeftSprite = playerRollLeftSprite;
            _playerRollRightSprite = playerRollRightSprite;
        }

        public void SetPlayerIdleSprite(ref SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = _playerIdleSprite;
        }

        public void SetPlayerMovingSprite(ref SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = _playerMovingSprite;
        }

        public void SetPlayerRollLeftSprite(ref SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = _playerRollLeftSprite;
        }

        public void SetPlayerRollRightSprite(ref SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = _playerRollRightSprite;
        }
    }
}