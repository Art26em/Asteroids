using Core.ObjectMovers;
using Zenject;

namespace Core.PlayerLogic.ControllerButtons
{
    public class RotateLeftButton : HoldingButton
    {
        private PlayerMover _playerMover;

        [Inject]
        private void Construct(PlayerMover playerMover)
        {
            _playerMover = playerMover;
        }

        private void Update()
        {
            if (IsHolding)
            {
                _playerMover.CalculateRotating(-1f);    
            }
        }
    }
}