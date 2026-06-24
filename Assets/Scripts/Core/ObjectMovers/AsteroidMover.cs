using System;
using System.Collections.Generic;
using System.Threading;
using Core.AsteroidsPresentation;
using Core.Physics;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.ObjectMovers
{
    public abstract class AsteroidMover : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private Dictionary<GameObject, CancellationTokenSource> _objectTokens = new();
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        protected async UniTask Move(GameObject gameObject)
        {
            if (!gameObject.TryGetComponent(out Asteroid asteroid)) return;
            
            var cancellationTokenSource = new CancellationTokenSource();
            _objectTokens.TryAdd(gameObject, cancellationTokenSource);
            
            var asteroidSpeedStats = asteroid.SpeedStats;
            var rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

            const int directionsCount = 2;
            const float clockWiseDirection = 1f;
            const float counterClockWiseDirection = -1f;
            var rotationDirection = Random.Range(0, directionsCount) == 0 
                ? clockWiseDirection : counterClockWiseDirection;
            
            var rotationAngle = 0f;

            asteroid.SpeedStats.CurrentVelocity = Random.insideUnitCircle.normalized * asteroidSpeedStats.MaxSpeed;
            
            try
            {
                while (true)
                {
                    rotationAngle = (rotationAngle + asteroidSpeedStats.RotationSpeed) * rotationDirection;

                    if (gameObject == null)
                    {
                        break;
                    }
                    
                    var newPosition = MovementPhysics.GetNewPosition(
                        rigidbody2D.position, 
                        asteroid.SpeedStats);
                    
                    rigidbody2D.MoveRotation(rotationAngle);
                    rigidbody2D.MovePosition(newPosition);
                    
                    if (!Application.isPlaying) break;
                    
                    await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationTokenSource.Token);
                }
            }
            catch (OperationCanceledException) {}
            finally{
                cancellationTokenSource.Dispose();
            }
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ObjectDisabledSignal>(OnObjectDisabled);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ObjectDisabledSignal>(OnObjectDisabled);
        }

        private void OnObjectDisabled(ObjectDisabledSignal signal)
        {
            if (!_objectTokens.TryGetValue(signal.GameObject, out var objectToken)) return;
            objectToken.Cancel();
            _objectTokens.Remove(signal.GameObject);
        }
        
    }
}