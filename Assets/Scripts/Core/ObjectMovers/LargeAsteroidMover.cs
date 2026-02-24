using System;
using System.Threading;
using Core.AsteroidsPresentation;
using Core.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.ObjectMovers
{
    public class LargeAsteroidMover : IMover<LargeAsteroid>
    {
        protected float MovingSpeedX;
        protected float MovingSpeedY;
        protected float RotationSpeed;
        
        private CancellationTokenSource _cancellationTokenSource;
        
        public LargeAsteroidMover(AsteroidsData asteroidsData)
        {
            MovingSpeedX = asteroidsData.LargeAsteroidMovingSpeedX;
            MovingSpeedY = asteroidsData.LargeAsteroidMovingSpeedY;
            RotationSpeed = asteroidsData.LargeAsteroidRotationSpeed;
        }
    
        public void StartObjectMoving(LargeAsteroid asteroid)
        { 
            _ = Move(asteroid.gameObject);    
        }
    
        protected async UniTask Move(GameObject gameObject)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var rotationDirection = Random.Range(0, 2) == 0 ? Vector3.forward : Vector3.back;
            var directionX = Random.Range(0, 2) == 0 ? MovingSpeedX : -MovingSpeedX;
            directionX *= Time.deltaTime;
            
            try
            {
                while (Application.isPlaying)
                {
                    gameObject.transform.position += new Vector3(directionX, -MovingSpeedY * Time.deltaTime, 0);
                    gameObject.transform.Rotate(rotationDirection, RotationSpeed * Time.deltaTime);
    
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