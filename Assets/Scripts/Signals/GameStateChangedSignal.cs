using Core.States;

namespace Signals
{
    public struct GameStateChangedSignal
    {
        public GameState NewGameState;

        public GameStateChangedSignal(GameState newGameState)
        {
            NewGameState = newGameState;
        }
    }
}