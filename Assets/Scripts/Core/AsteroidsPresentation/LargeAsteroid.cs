using Core.Configs;
using Core.SpeedSystem;
using Signals;
using UnityEngine;
using Zenject;

namespace Core.AsteroidsPresentation
{
    public class LargeAsteroid : Asteroid
    {
        [Inject]
        private void Construct(AsteroidsData asteroidsData)
        {
            SpeedStats = new SpeedStats
            {
                MaxSpeed = asteroidsData.LargeAsteroidSpeedStats.MaxSpeed,
                Acceleration = asteroidsData.LargeAsteroidSpeedStats.Acceleration,
                Deceleration = asteroidsData.LargeAsteroidSpeedStats.Deceleration,
                RotationSpeed = asteroidsData.LargeAsteroidSpeedStats.RotationSpeed
            };
            Score = asteroidsData.LargeAsteroidScore;
        }

        protected override void FireSignals(Collision2D other)
        {
            SignalBus.Fire(new LargeAsteroidDestroyedSignal(other.gameObject.transform));
            SignalBus.Fire(new ScoreIncreasedSignal(Score));
            SignalBus.Fire(new ObjectDisabledSignal(gameObject));
        }
    }
}