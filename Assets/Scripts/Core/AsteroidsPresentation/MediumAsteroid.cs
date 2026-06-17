using Core.Configs;
using Core.SpeedSystem;
using Signals;
using UnityEngine;
using Zenject;

namespace Core.AsteroidsPresentation
{
    public class MediumAsteroid : Asteroid
    {
        [Inject]
        private void Construct(AsteroidsData asteroidsData, SignalBus signalBus)
        {
            SpeedStats = new SpeedStats
            {
                MaxSpeed = asteroidsData.MediumAsteroidSpeedStats.MaxSpeed,
                Acceleration = asteroidsData.MediumAsteroidSpeedStats.Acceleration,
                Deceleration = asteroidsData.MediumAsteroidSpeedStats.Deceleration,
                RotationSpeed = asteroidsData.MediumAsteroidSpeedStats.RotationSpeed
            };
            Score = asteroidsData.MediumAsteroidScore;
        }

        protected override void FireSignals(Collision2D other)
        {
            SignalBus.Fire(new ScoreIncreasedSignal(Score));
        }
    }
}