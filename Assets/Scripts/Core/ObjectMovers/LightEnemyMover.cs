using System;
using System.Threading;
using Core.EnemiesPresentation;
using Core.Physics;
using Core.PlayerPresentation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.ObjectMovers
{
    public class LightEnemyMover
    {
        private PlayerObject _playerObject;
        
        [Inject]
        private void Construct(PlayerObject playerObject)
        {
            _playerObject = playerObject;
        }
        
        public void StartObjectMoving(GameObject gameObject)
        {
            _ = Move(gameObject);    
        }

        private async UniTask Move(GameObject gameObject)
        {
            if (!gameObject.TryGetComponent(out LightEnemy lightEnemy)) return;

            var cancellationTokenSource = new CancellationTokenSource();
            var rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            
            try
            {
                while (Application.isPlaying && gameObject.activeInHierarchy)
                {
                    lightEnemy.SpeedStats.CurrentVelocity = MovementPhysics.GetNewSeekingVelocity(
                            rigidbody2D.position,
                            _playerObject.gameObject.transform.position,
                            lightEnemy.SpeedStats);

                    var newPosition = MovementPhysics.GetNewPosition(
                        rigidbody2D.position, 
                        lightEnemy.SpeedStats);
                    
                    rigidbody2D.MovePosition(newPosition);
                    
                    Vector2 direction = _playerObject.transform.position - gameObject.transform.position;
                    gameObject.transform.up = -direction;
                    
                    await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationTokenSource.Token);
                    if (!Application.isPlaying) break;
                }
            }
            catch (OperationCanceledException) {}
            finally{
                cancellationTokenSource.Cancel();
            }
        }
    }
}