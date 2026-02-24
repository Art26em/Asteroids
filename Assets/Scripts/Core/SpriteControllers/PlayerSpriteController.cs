using Core.PlayerPresentation;
using UnityEngine;

namespace Core.SpriteControllers
{
    public class PlayerSpriteController
    {
        private readonly Sprite _playerIdleSprite;
        private readonly Sprite _playerMovingSprite;
        private readonly SpriteRenderer _playerSpriteRenderer;

        public PlayerSpriteController(Sprite[] playerIdleMovingSprites, PlayerObject playerObject)
        {
            if (playerIdleMovingSprites.Length > 1)
            {
                _playerIdleSprite = playerIdleMovingSprites[0];
                _playerMovingSprite = playerIdleMovingSprites[1];    
            }
            _playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();
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