using Core.States;

namespace Signals
{
    public struct GameStateChangedSignal
    {
        private GameState _newGameState;

        public GameStateChangedSignal(GameState newGameState)
        {
            _newGameState = newGameState;
        }
    }
}