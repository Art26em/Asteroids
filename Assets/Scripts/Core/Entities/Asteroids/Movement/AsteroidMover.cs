using System;
using System.Threading;
using Core.Configs;
using Core.World;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Core.Entities.Asteroids.Movement
{
    public class AsteroidMover
    {
        private readonly float _movingSpeedX;
        private readonly float _movingSpeedY;
        private readonly float _rotationSpeed;
        private readonly WorldBoundsChecker _worldBoundsChecker;
        private CancellationTokenSource _cancellationTokenSource;
        
        public AsteroidMover(AsteroidsData asteroidsData, WorldBoundsChecker worldBoundsChecker)
        {
            _movingSpeedX = asteroidsData.MovingSpeedX;
            _movingSpeedY = asteroidsData.MovingSpeedY;
            _rotationSpeed = asteroidsData.RotationSpeed;
            _worldBoundsChecker = worldBoundsChecker;
        }

        public void StartMoving(Asteroid asteroid)
        { 
            _ = Move(asteroid.gameObject);    
        }

        private async UniTask Move(GameObject gameObject)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var rotationDirection = new Random().Next(2) == 0 ? Vector3.forward : Vector3.back;
            var directionX = new Random().Next(2) == 0 ? _movingSpeedX : -_movingSpeedX;
            directionX *= Time.deltaTime;
            
            try
            {
                while (Application.isPlaying)
                {
                    gameObject.transform.position += new Vector3(directionX, -_movingSpeedY * Time.deltaTime, 0);
                    gameObject.transform.Rotate(rotationDirection, _rotationSpeed * Time.deltaTime);

                    var newPos = _worldBoundsChecker.GetObjectWorldPosition(gameObject.transform.position);
                    gameObject.transform.position = newPos;
                    
                    await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                    if (!Application.isPlaying) break;
                }
                SafeCancelAndDispose();
            }
            catch (OperationCanceledException) {}
        }
        
        private void SafeCancelAndDispose()
        {
            if (_cancellationTokenSource == null) return;
            
            try
            {
                if (!_cancellationTokenSource.IsCancellationRequested)
                {
                     _cancellationTokenSource.Cancel();
                }
                _cancellationTokenSource.Dispose();
            }
            catch (ObjectDisposedException) {}
            _cancellationTokenSource = null;
        }
        
    }
}