using System;
using System.Threading;
using Core.AsteroidsPresentation;
using Core.Physics;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.ObjectMovers
{
    public class LargeAsteroidMover
    {
        public void StartObjectMoving(GameObject gameObject)
        {
            _ = Move(gameObject);    
        }

        private async UniTask Move(GameObject gameObject)
        {
            if (!gameObject.TryGetComponent(out LargeAsteroid asteroid)) return;

            var cancellationTokenSource = new CancellationTokenSource();
            
            var asteroidSpeedStats = asteroid.SpeedStats;
            var rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            var rotationDirection = Random.Range(0, 2) == 0 ? 1f : -1f;
            var rotationAngle = 0f;

            asteroid.SpeedStats.CurrentVelocity = Random.insideUnitCircle.normalized * asteroidSpeedStats.MaxSpeed;
            
            try
            {
                while (Application.isPlaying && gameObject.activeInHierarchy)
                {
                    rotationAngle = (rotationAngle + asteroidSpeedStats.RotationSpeed) * rotationDirection;
                    
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
                cancellationTokenSource.Cancel();
            }
        }
    }
}