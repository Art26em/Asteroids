using System;
using System.Threading;
using Core.AnimationsSettings;
using Core.PlayerPresentation;
using Core.SpriteControllers;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using Zenject;

namespace Core.AnimationsControllers
{
    public class AnimationsController
    {
        private EarthAnimationSettings _earthAnimationSettings;
        private PlayerObject _playerObject;
        private PlayerAnimationSettings _playerAnimationSettings;
        private PlayerSpriteController _playerSpriteController;
        private ParticleSystem _space;
        private SignalBus _signalBus;
        
        private CancellationTokenSource _earthAnimCancellationTokenSource;
        private CancellationTokenSource _playerAnimCancellationTokenSource;

        [Inject]
        private void Construct(
            EarthAnimationSettings earthAnimationSettings,
            PlayerObject playerObject,
            PlayerAnimationSettings playerAnimationSettings,
            PlayerSpriteController playerSpriteController,
            SignalBus signalBus,
            ParticleSystem space)
        {
            _earthAnimationSettings = earthAnimationSettings;
            _earthAnimationSettings.Earth.position = _earthAnimationSettings.EarthStartPosition;
            
            _playerObject = playerObject;
            _playerAnimationSettings = playerAnimationSettings;
            _playerSpriteController = playerSpriteController;
            _signalBus = signalBus;
            _space = space;
        }
        
        public void OnGameStart()
        {
            _earthAnimationSettings.Earth.gameObject.SetActive(true);
            _earthAnimationSettings.Earth.position = _earthAnimationSettings.EarthStartPosition;
            _playerObject.transform.position = _playerAnimationSettings.PlayerStartPosition;
            
            _ = MoveOutEarth();
            _ = MoveInPlayer();
        }

        private async UniTask MoveOutEarth()
        {
            var journeyLength = Vector2.Distance(
                _earthAnimationSettings.EarthStartPosition,
                _earthAnimationSettings.EarthTargetPosition);
            var startTime = Time.time;

            _earthAnimCancellationTokenSource = new CancellationTokenSource();

            try
            {
                while (_earthAnimationSettings.Earth.position != _earthAnimationSettings.EarthTargetPosition)
                {
                    // Рассчитываем пройденное расстояние
                    var distanceCovered = (Time.time - startTime) * _earthAnimationSettings.EarthMoveOutSpeed;
                    var fractionOfJourney = distanceCovered / journeyLength;

                    // Плавное перемещение с использованием Lerp
                    _earthAnimationSettings.Earth.position = Vector3.Lerp(
                        _earthAnimationSettings.EarthStartPosition,
                        _earthAnimationSettings.EarthTargetPosition,
                        fractionOfJourney);

                    // Если достигли цели, выходим
                    if (fractionOfJourney >= 1f)
                    {
                        _earthAnimationSettings.Earth.position = _earthAnimationSettings.EarthTargetPosition;
                        _earthAnimationSettings.Earth.gameObject.SetActive(false);
                        _space.Pause();
                        _signalBus.Fire<StartAnimationCompletedSignal>();
                        break;
                    }

                    // Ждем один кадр
                    await UniTask.Yield(PlayerLoopTiming.Update, _earthAnimCancellationTokenSource.Token);
                }
                _earthAnimCancellationTokenSource.Cancel();
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                _earthAnimCancellationTokenSource.Dispose();
            }
        }

        private async UniTask MoveInPlayer()
        {
            var journeyLength = Vector2.Distance(
                _playerAnimationSettings.PlayerStartPosition,
                _playerAnimationSettings.PlayerTargetPosition);
            var startTime = Time.time;
            
            // Отключаем возможность перемещаться во время анимации
            _playerObject.IsInputEnabled = false;
            _playerSpriteController.SetPlayerMovingSprite();
            
            _playerAnimCancellationTokenSource = new CancellationTokenSource();

            try
            {
                while (_playerObject.transform.position != _playerAnimationSettings.PlayerTargetPosition)
                {
                    // Рассчитываем пройденное расстояние
                    var distanceCovered = (Time.time - startTime) * _playerAnimationSettings.PlayerMoveInSpeed;
                    var fractionOfJourney = distanceCovered / journeyLength;

                    // Плавное перемещение с использованием Lerp
                    _playerObject.transform.position = Vector2.Lerp(
                        _playerAnimationSettings.PlayerStartPosition,
                        _playerAnimationSettings.PlayerTargetPosition,
                        fractionOfJourney);

                    // Если достигли цели, выходим
                    if (fractionOfJourney >= 1f)
                    {
                        _playerObject.transform.position = _playerAnimationSettings.PlayerTargetPosition;
                        _playerObject.IsInputEnabled = true;
                        _playerSpriteController.SetPlayerIdleSprite();
                        break;
                    }

                    // Ждем один кадр
                    await UniTask.Yield(PlayerLoopTiming.Update, _playerAnimCancellationTokenSource.Token);
                }
                _playerAnimCancellationTokenSource.Cancel();
            }
            catch (OperationCanceledException)
            {
            }
            finally{
                _playerAnimCancellationTokenSource.Dispose();
            }
        }
    }
}