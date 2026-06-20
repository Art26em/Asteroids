using Core.Analytics;
using Core.ScoreSystem;
using UI.Views;
using Zenject;

namespace Core.StateControllers
{
    public class GameOverController
    {
        private GameOverScreen _gameOverScreen;
        private AnalyticsEventSender _analyticsEventSender;
        private Score _score;
        
        [Inject]
        private void Construct(
            GameOverScreen gameOverScreen, 
            AnalyticsEventSender analyticsEventSender,
            Score score)
        {
            _gameOverScreen = gameOverScreen;
            _analyticsEventSender = analyticsEventSender;
            _score = score;
        }

        public void OnGameOver()
        {
            _gameOverScreen.gameObject.SetActive(true);
            _analyticsEventSender.PlayerDiedEvent(_score.CurrentScore());
        }
    }
}