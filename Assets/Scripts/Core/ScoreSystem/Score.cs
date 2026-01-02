namespace Core.ScoreSystem
{
    public class Score
    {
        private int _scoreCount;

        public int GetCurrentScore()
        {
            return _scoreCount;
        }

        public void AddScore(int score)
        {
            _scoreCount += score;
        }
    }
}