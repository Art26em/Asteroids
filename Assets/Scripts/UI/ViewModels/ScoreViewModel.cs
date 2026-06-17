using System;
using Core.ScoreSystem;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class ScoreViewModel : IInitializable, IDisposable
    {
        [Data("Score")]
        public readonly ReactiveProperty<string> ScoreView = new();
        private Score _score;

        [Inject]
        private void Construct(Score score)
        {
            _score = score;
        }

        public void Initialize()
        {
            OnScoreChanged(_score.CurrentScore());
            _score.OnScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged(int score)
        {
            ScoreView.Value = score.ToString();
        }
        
        public void Dispose()
        {
            _score.OnScoreChanged -= OnScoreChanged;
        }
    }
}