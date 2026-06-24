using System;

namespace Core.ScoreSystem
{
    public class Score
    {
        public int CurrentScore { get; private set; }

        public event Action<int> OnScoreChanged;
        
        public void AddScore(int score)
        {
            if (score <= 0) return;
            CurrentScore += score;
            OnScoreChanged?.Invoke(CurrentScore);
        }
    }
}