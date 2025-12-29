using UnityEngine;

namespace Core.AnimationsControllers
{
    public class PlayerAnimationSettings
    {
        public Sprite PlayerIdleSprite {get; private set;}
        public Sprite PlayerMovingSprite {get; private set;}
        public Sprite PlayerRollLeftSprite {get; private set;}
        public Sprite PlayerRollRightSprite {get; private set;}

        public PlayerAnimationSettings(Sprite playerIdleSprite, Sprite playerMovingSprite, Sprite playerRollLeftSprite, Sprite playerRollRightSprite)
        {
            PlayerIdleSprite = playerIdleSprite;
            PlayerMovingSprite = playerMovingSprite;
            PlayerRollLeftSprite = playerRollLeftSprite;
            PlayerRollRightSprite = playerRollRightSprite;
        }
    }
}