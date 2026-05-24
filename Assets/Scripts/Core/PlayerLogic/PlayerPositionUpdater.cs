using Core.ObjectMovers;
using UnityEngine;
using Zenject;

namespace Core.PlayerLogic
{
    public class PlayerPositionUpdater : MonoBehaviour
    {
        private PlayerMover _playerMover;

        [Inject]
        private void Construct(PlayerMover playerMover)
        {
            _playerMover = playerMover;
        }

        private void FixedUpdate()
        {
            _playerMover.CalculatePosition();
        }
    }
}