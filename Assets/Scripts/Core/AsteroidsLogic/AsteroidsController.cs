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
            var cancellationTokenSource = new CancellationTokenSource();
            try
            {
                while (Application.isPlaying && !cancellationTokenSource.IsCancellationRequested)
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
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationTokenSource.Token);
                }
            }
            catch (OperationCanceledException) {}
            finally
            {
                cancellationTokenSource.Dispose();
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

            for (var i = 0; i < _asteroidsData.MediumAsteroidCount; i++)
            {
                if (_mediumAsteroidSpawner.TrySpawnObject(spawnPoint, out var spawnedObject))
                {
                    _mediumAsteroidMover.StartObjectMoving(spawnedObject.gameObject);    
                }
            }    
            
        }
        
    }
}