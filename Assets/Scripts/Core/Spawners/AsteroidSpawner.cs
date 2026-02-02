using System;
using System.Threading;
using Core.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.Spawners
{
    public class AsteroidSpawner
    {
        private AsteroidFactory _factory;
        private float _timeToSpawn;
        private Vector2[] _spawnPositions;

        private CancellationTokenSource _cancellationTokenSource;
        private float _elapsedTime;
        
        [Inject]
        private void Construct(AsteroidFactory factory,  float timeToSpawn, Vector2[] spawnPositions)
        {
            _factory = factory;
            _timeToSpawn = timeToSpawn;
            _spawnPositions = spawnPositions;
        }

        private bool IsTimeToSpawn()
        {
            if (_elapsedTime >= _timeToSpawn)
            {
                _elapsedTime = 0;
                return true;
            }   
            return false;
        }

        public void StartSpawning()
        {
            _ = SpawnAsteroids();
        }
        
        private async UniTask SpawnAsteroids()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _elapsedTime = 0;
            try
            {
                while (true)
                {
                    _elapsedTime += Time.deltaTime;
                    if (IsTimeToSpawn())
                    {
                        var asteroid = _factory.Create();
                    } 
                    await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                }
                
            }
            catch (OperationCanceledException) {}
        }
        
        private void OnDestroy()
        {
            SafeCancelAndDispose();
        }
        
        private void SafeCancelAndDispose()
        {
            if (_cancellationTokenSource == null) return;
            
            try
            {
                if (!_cancellationTokenSource.IsCancellationRequested)
                    _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
            catch (ObjectDisposedException) {}
            _cancellationTokenSource = null;
        }
        
    }
}