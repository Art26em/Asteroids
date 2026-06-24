using System;
using System.Threading;
using Core.AsteroidsPresentation;
using Core.Configs;
using Core.ObjectMovers;
using Core.ObjectSpawners;
using Core.States;
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
        private CancellationTokenSource _cancellationTokenSource;
        
        private SignalBus _signalBus;
        private float _elapsedTime;
        private bool _isGameOver;
        
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
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        }

        private async UniTask SpawnAsteroids()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                while (Application.isPlaying && !_cancellationTokenSource.IsCancellationRequested)
                {
                    if (_largeAsteroidSpawner.IsSpawnIntervalElapsed(_elapsedTime, _asteroidsData.TimeToSpawn))
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
            }
            catch (OperationCanceledException) {}
            finally
            {
                _cancellationTokenSource.Dispose();
                _signalBus.Unsubscribe<LargeAsteroidDestroyedSignal>(OnLargeAsteroidDestroyed);
            }
        }

        private void OnLargeAsteroidDestroyed(LargeAsteroidDestroyedSignal signal)
        {
            _ = SpawnObjectsWithDelay(signal.AsteroidTransform);
        }
        
        private async UniTask SpawnObjectsWithDelay(Transform spawnPoint)
        {
            var elapsedTime = 0f;
            var delay = _asteroidsData.MediumAsteroidSpawnDelay;
            while (elapsedTime < delay)
            {
                elapsedTime += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            }

            if (_isGameOver) return;
            
            for (var i = 0; i < _asteroidsData.MediumAsteroidCount; i++)
            {
                if (_mediumAsteroidSpawner.TrySpawnObject(spawnPoint, out var spawnedObject))
                {
                    _mediumAsteroidMover.StartObjectMoving(spawnedObject.gameObject);    
                }
            }    
            
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.NewGameState != GameState.GameOver) return;
            _isGameOver = true;
            _cancellationTokenSource.Cancel();
        }
        
    }
}