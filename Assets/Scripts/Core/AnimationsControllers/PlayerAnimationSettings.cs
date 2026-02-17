using Core.Entities.Player;
using UnityEngine;

namespace Core.AnimationsControllers
{
    public class PlayerAnimationSettings
    {
        public readonly PlayerObject Player;
        public readonly float PlayerMoveInSpeed;
        public Vector3 PlayerStartPosition;
        public Vector3 PlayerTargetPosition;
        
        public PlayerAnimationSettings(
            PlayerObject player,
            float playerMoveInSpeed,
            Vector3[] playerStartTargetPositions)
        {
            Player = player;
            PlayerMoveInSpeed = playerMoveInSpeed;
            if (playerStartTargetPositions == null || playerStartTargetPositions.Length < 2)
            {
                PlayerStartPosition = new Vector3(0, 0, 0);
                PlayerTargetPosition = new Vector3(0, 0, 0); 
            }
            else
            {
                PlayerStartPosition = playerStartTargetPositions[0];
                PlayerTargetPosition = playerStartTargetPositions[1];
            }
        }
        
    }
}