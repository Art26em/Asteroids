using UnityEngine;

namespace Core.SpriteControllers
{
    public class PlayerSpriteController
    {
        private readonly Sprite _playerIdleSprite;
        private readonly Sprite _playerMovingSprite;
        private readonly SpriteRenderer _playerSpriteRenderer;

        public PlayerSpriteController(
            Sprite playerIdleSprite, 
            Sprite playerMovingSprite, 
            SpriteRenderer playerSpriteRenderer)
        {
            _playerIdleSprite = playerIdleSprite;
            _playerMovingSprite = playerMovingSprite;
            _playerSpriteRenderer = playerSpriteRenderer;
        }

        public void UpdatePlayerSprite()
        {
            var isMovingY = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || 
                            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow);
            
            switch (isMovingY)
            {
                case true when _playerSpriteRenderer.sprite != _playerMovingSprite:
                    SetPlayerMovingSprite();
                    break;
                case false when _playerSpriteRenderer.sprite != _playerIdleSprite:
                    SetPlayerIdleSprite();
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
        
    }
}