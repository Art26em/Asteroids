using System;
using System.Threading;
using Core.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.ObjectMovers
{
    public class MediumAsteroidMover
    {
        private float _movingSpeedX;
        private float _movingSpeedY;
        private float _rotationSpeed;
        
        private CancellationTokenSource _cancellationTokenSource;

        [Inject]
        private void Construct(AsteroidsData asteroidsData)
        {
            _movingSpeedX = asteroidsData.MediumAsteroidMovingSpeedX;
            _movingSpeedY = asteroidsData.MediumAsteroidMovingSpeedY;
            _rotationSpeed = asteroidsData.MediumAsteroidRotationSpeed;    
        }
        
        public void StartObjectMoving(GameObject gameObject)
        {
            _ = Move(gameObject);    
        }

        private async UniTask Move(GameObject gameObject)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var rotationDirection = Random.Range(0, 2) == 0 ? Vector3.forward : Vector3.back;
            var directionX = Random.Range(0, 2) == 0 ? _movingSpeedX : -_movingSpeedX;
            var directionY = Random.Range(0, 2) == 0 ? _movingSpeedY : -_movingSpeedY;
            
            directionX *= Time.deltaTime;
            directionY *= Time.deltaTime;
            
            try
            {
                while (Application.isPlaying)
                {
                    gameObject.transform.position += new Vector3(directionX, directionY, 0);
                    gameObject.transform.Rotate(rotationDirection, _rotationSpeed * Time.deltaTime);
    
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