using System;
using System.Threading;
using Core.Configs;
using Core.EnemiesPresentation;
using Core.ObjectMovers;
using Core.ObjectSpawners;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.EnemiesLogic
{
    public class EnemiesController
    {
        private ObjectSpawner<LightEnemy> _lightEnemySpawner;
        private LightEnemyMover _lightEnemyMover;
        private EnemiesData _enemiesData;
        
        private SignalBus _signalBus;
        private float _elapsedTime;
        
        [Inject]
        public void Construct(
            ObjectSpawner<LightEnemy> lightEnemySpawner,
            LightEnemyMover lightEnemyMover,
            EnemiesData enemiesData,
            SignalBus signalBus)
        {
            _lightEnemySpawner = lightEnemySpawner;
            _lightEnemyMover = lightEnemyMover;
            _enemiesData = enemiesData;
            _signalBus = signalBus;
        }

        public void StartEnemiesSpawning()
        {
            _ = SpawnEnemies();
            _signalBus.Subscribe<LightEnemyDiedSignal>(OnLightEnemyDied);
        }

        private async UniTask SpawnEnemies()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            try
            {
                while (Application.isPlaying && !cancellationTokenSource.IsCancellationRequested)
                {
                    if (_lightEnemySpawner.IsTimeToSpawn(_elapsedTime, _enemiesData.LightEnemyTimeToSpawn))
                    {
                        var pointIndex = Random.Range(0, _enemiesData.EnemiesSpawnPoints.Length);
                        var spawnPoint = _enemiesData.EnemiesSpawnPoints[pointIndex];
                        if (_lightEnemySpawner.TrySpawnObject(spawnPoint, out var spawnedObject))
                        {
                            _lightEnemyMover.StartObjectMoving(spawnedObject.gameObject);
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

        private void OnLightEnemyDied(LightEnemyDiedSignal signal)
        {
                
        }
    }
}