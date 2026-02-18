using System;
using System.Threading;
using Core.Configs;
using Core.ObjectMovers;
using Core.World;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Core.Entities.Asteroids.Movement
{
    public class LargeAsteroidMover : IMover<LargeAsteroid>
    {
        protected float MovingSpeedX;
        protected float MovingSpeedY;
        protected float RotationSpeed;
        private readonly WorldBoundsChecker _worldBoundsChecker;
        private CancellationTokenSource _cancellationTokenSource;
        
        public LargeAsteroidMover(AsteroidsData asteroidsData, WorldBoundsChecker worldBoundsChecker)
        {
            MovingSpeedX = asteroidsData.LargeAsteroidMovingSpeedX;
            MovingSpeedY = asteroidsData.LargeAsteroidMovingSpeedY;
            RotationSpeed = asteroidsData.LargeAsteroidRotationSpeed;
            _worldBoundsChecker = worldBoundsChecker;
        }

        public void StartObjectMoving(LargeAsteroid asteroid)
        { 
            _ = Move(asteroid.gameObject);    
        }

        protected async UniTask Move(GameObject gameObject)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var rotationDirection = new Random().Next(2) == 0 ? Vector3.forward : Vector3.back;
            var directionX = new Random().Next(2) == 0 ? MovingSpeedX : -MovingSpeedX;
            directionX *= Time.deltaTime;
            
            try
            {
                while (Application.isPlaying)
                {
                    gameObject.transform.position += new Vector3(directionX, -MovingSpeedY * Time.deltaTime, 0);
                    gameObject.transform.Rotate(rotationDirection, RotationSpeed * Time.deltaTime);

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