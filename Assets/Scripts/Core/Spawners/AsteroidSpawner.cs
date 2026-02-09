using System;
using System.Threading;
using Core.Entities.Asteroids.Movement;
using Core.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Spawners
{
    public class AsteroidSpawner
    {
        private readonly AsteroidFactory _factory;
        private readonly AsteroidMover _mover;
        private readonly Transform[] _spawnPositions;
        private readonly float _spawnTime;

        private CancellationTokenSource _cancellationTokenSource;
        private float _elapsedTime;

        public AsteroidSpawner(AsteroidFactory factory, AsteroidMover mover, Transform[] spawnPositions, float spawnTime)
        {
            _factory = factory;
            _mover = mover;
            _spawnPositions = spawnPositions;
            _spawnTime = spawnTime;
        }

        private bool IsTimeToSpawn()
        {
            if (_elapsedTime < _spawnTime) return false;
            _elapsedTime = 0;
            return true;
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
                while (Application.isPlaying)
                {
                    _elapsedTime += Time.deltaTime;
                    if (IsTimeToSpawn())
                    {
                        var asteroid = _factory.Create();
                        asteroid.transform.position = _spawnPositions[Random.Range(0, _spawnPositions.Length)].position;
                        _mover.StartMoving(asteroid);
                    } 
                    await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                    if (!Application.isPlaying) break; 
                }
                
            }
            catch (OperationCanceledException) {}
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