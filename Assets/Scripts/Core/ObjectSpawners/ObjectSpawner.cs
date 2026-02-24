using System;
using System.Threading;
using Core.Factories;
using Core.ObjectMovers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Spawners
{
    public class ObjectSpawner<T> where T : Component
    {
        private readonly ObjectFactory<T> _factory;
        private readonly IMover<T> _mover;
        private Transform[] _spawnPositions;
        private readonly float _spawnTime;

        private CancellationTokenSource _cancellationTokenSource;
        private float _elapsedTime;
        
        public ObjectSpawner(ObjectFactory<T> factory, IMover<T> mover, Transform[] spawnPositions, float spawnTime = 0)
        {
            _factory = factory;
            _mover = mover;
            _spawnPositions = spawnPositions;
            _spawnTime = spawnTime;
        }

        public void SetSpawnPositions(Transform[] spawnPositions)
        {
            _spawnPositions = spawnPositions;
        }
        
        private bool IsTimeToSpawn()
        {
            if (_elapsedTime < _spawnTime) return false;
            _elapsedTime = 0;
            return true;
        }

        public void StartObjectsSpawning(bool autoSpawn = true)
        {
            if (autoSpawn)
            {
                _ = SpawnObjects();
            }
            else
            {
                foreach (var spawnPoint in _spawnPositions)
                {
                    if (!_factory.TryCreateObject(out var item)) break;
                    item.gameObject.SetActive(true);
                    item.transform.position = spawnPoint.position;
                    item.transform.rotation = spawnPoint.rotation;
                    _mover.StartObjectMoving(item);
                }
            }
        }
        
        private async UniTask SpawnObjects()
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
                        if (_factory.TryCreateObject(out var item))
                        {
                            var spawnPoint = _spawnPositions[Random.Range(0, _spawnPositions.Length)]; 
                            item.transform.position = spawnPoint.position;
                            _mover.StartObjectMoving(item);    
                        }    
                    }
                    if (!Application.isPlaying) break;
                    await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
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
                    _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
            catch (ObjectDisposedException) {}
            _cancellationTokenSource = null;
        }
        
    }
}