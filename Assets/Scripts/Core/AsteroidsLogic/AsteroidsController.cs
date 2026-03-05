using System;
using System.Threading;
using Core.AsteroidsPresentation;
using Core.Configs;
using Core.ObjectMovers;
using Core.ObjectSpawners;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.AsteroidsLogic
{
    public class AsteroidsController
    {
        private ObjectSpawner<LargeAsteroid> _largeAsteroidSpawner;
        private LargeAsteroidMover _largeAsteroidMover;
        private ObjectSpawner<MediumAsteroid> _mediumAsteroidSpawner;
        private MediumAsteroidMover _mediumAsteroidMover;
        private AsteroidsData _asteroidsData;
        
        private SignalBus _signalBus;
        private float _elapsedTime;
        private CancellationTokenSource _cancellationTokenSource;
        
        [Inject]
        public void Construct(
            ObjectSpawner<LargeAsteroid> largeAsteroidSpawner,
            LargeAsteroidMover mover,
            ObjectSpawner<MediumAsteroid> mediumAsteroidSpawner,
            MediumAsteroidMover mediumAsteroidMover,
            AsteroidsData asteroidsData,
            SignalBus signalBus)
        {
            _largeAsteroidSpawner = largeAsteroidSpawner;
            _largeAsteroidMover = mover;
            _mediumAsteroidSpawner = mediumAsteroidSpawner;
            _mediumAsteroidMover = mediumAsteroidMover;
            _asteroidsData = asteroidsData;
            _signalBus = signalBus;
        }

        public void StartAsteroidsSpawning()
        {
            _ = SpawnAsteroids();
            _signalBus.Subscribe<LargeAsteroidDestroyedSignal>(OnLargeAsteroidDestroyed);
        }

        private async UniTask SpawnAsteroids()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                while (Application.isPlaying && !_cancellationTokenSource.IsCancellationRequested)
                {
                    if (_largeAsteroidSpawner.IsTimeToSpawn(_elapsedTime, _asteroidsData.TimeToSpawn))
                    {
                        var pointIndex = Random.Range(0, _asteroidsData.AsteroidSpawnPositions.Length);
                        var spawnPoint = _asteroidsData.AsteroidSpawnPositions[pointIndex];
                        if (_largeAsteroidSpawner.TrySpawnObject(spawnPoint, out var spawnedObject))
                        {
                            _largeAsteroidMover.StartObjectMoving(spawnedObject.gameObject);    
                        }
                        _elapsedTime = 0;
                    }
                    _elapsedTime += Time.deltaTime;  
                    await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                }
                SafeCancelAndDispose();
            }
            catch (OperationCanceledException){} 
        }

        private void OnLargeAsteroidDestroyed(LargeAsteroidDestroyedSignal signal)
        {
            for (var i = 0; i < _asteroidsData.MediumAsteroidCount; i++)
            {
                if (_mediumAsteroidSpawner.TrySpawnObject(signal.AsteroidTransform, out var spawnedObject))
                {
                    _mediumAsteroidMover.StartObjectMoving(spawnedObject.gameObject);    
                }
            }        
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