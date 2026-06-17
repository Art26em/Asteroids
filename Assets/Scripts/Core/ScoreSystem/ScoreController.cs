using System;
using Signals;
using Zenject;

namespace Core.ScoreSystem
{
    public class ScoreController : IInitializable, IDisposable
    {
        private Score _score;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(Score score, SignalBus signalBus)
        {
            _score = score;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ScoreIncreasedSignal>(OnScoreIncreased);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ScoreIncreasedSignal>(OnScoreIncreased);
        }
        
        private void OnScoreIncreased(ScoreIncreasedSignal signal)
        {
            _score.AddScore(signal.AddedScore);
        }
        
    }
}