using System;

namespace Core.ScoreSystem
{
    public class Score
    {
        private int _scoreCount;
        public int GetCurrentScore() => _scoreCount;
        public event Action<int> OnScoreChanged;
        
        public void AddScore(int score = 1)
        {
            if (score <= 0) return;
            _scoreCount += score;
            OnScoreChanged?.Invoke(_scoreCount);
        }
    }
}