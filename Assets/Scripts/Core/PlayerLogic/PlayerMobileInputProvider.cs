
using Zenject;

namespace Core.PlayerLogic
{
    public class PlayerMobileInputProvider : IPlayerInputProvider
    {
        private PlayerInputData _playerInputData;

        [Inject]
        public void Construct(PlayerInputData playerInputData)
        {
            _playerInputData = playerInputData;
        }
        
        public PlayerInputData GetPlayerInput()
        {
            return _playerInputData;
        }
    }
}