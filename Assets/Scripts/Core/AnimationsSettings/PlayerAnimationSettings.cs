using Core.PlayerPresentation;
using UnityEngine;

namespace Core.AnimationsSettings
{
    public class PlayerAnimationSettings
    {
        public readonly float PlayerMoveInSpeed;
        public Vector3 PlayerStartPosition;
        public Vector3 PlayerTargetPosition;
        
        public PlayerAnimationSettings(float playerMoveInSpeed, Vector3[] playerStartTargetPositions)
        {
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