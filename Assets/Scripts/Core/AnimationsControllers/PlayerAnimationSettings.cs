using UnityEngine;

namespace Core.AnimationsControllers
{
    public class PlayerAnimationSettings
    {
        public readonly GameObject Player;
        public readonly float PlayerMoveInSpeed;
        public Vector3 PlayerStartPosition;
        public Vector3 PlayerTargetPosition;
        
        public PlayerAnimationSettings(
            GameObject player,
            float playerMoveInSpeed,
            Vector3 playerStartPosition,
            Vector3 playerTargetPosition)
        {
            Player = player;
            PlayerMoveInSpeed = playerMoveInSpeed;
            PlayerStartPosition = playerStartPosition;
            PlayerTargetPosition = playerTargetPosition;
        }
        
        public SpriteRenderer GetPlayerSpriteRenderer()
        {
            return Player.GetComponent<SpriteRenderer>();
        }
        
    }
}