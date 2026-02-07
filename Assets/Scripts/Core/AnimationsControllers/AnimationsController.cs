using System;
using System.Threading;
using Core.AnimationsSettings;
using Core.Entities.Player.Controllers;
using Core.SpriteControllers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.AnimationsControllers
{
    public class AnimationsController
    {
        private readonly EarthAnimationSettings _earthAnimationSettings;
        private readonly PlayerAnimationSettings _playerAnimationSettings;
        private readonly PlayerSpriteController _playerSpriteController;
        private readonly PlayerInputController _playerInputController;
        
        private CancellationTokenSource _earthAnimCancellationTokenSource;
        private CancellationTokenSource _playerAnimCancellationTokenSource;
        
        public AnimationsController(
            EarthAnimationSettings earthAnimationSettings,
            PlayerAnimationSettings playerAnimationSettings, 
            PlayerSpriteController playerSpriteController)
        {
            _earthAnimationSettings = earthAnimationSettings;
            _earthAnimationSettings.Earth.position = _earthAnimationSettings.EarthStartPosition;
            _playerAnimationSettings = playerAnimationSettings;
            _playerInputController = _playerAnimationSettings.Player.GetComponent<PlayerInputController>();
            _playerSpriteController = playerSpriteController;
        }

        public void OnGameStart()
        {
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
                        break;
                    }

                    // Ждем один кадр
                    await UniTask.Yield(PlayerLoopTiming.Update, _earthAnimCancellationTokenSource.Token);
                }
            }
            catch (OperationCanceledException) {}
            
        }

        private async UniTask MoveInPlayer()
        {
            var journeyLength = Vector2.Distance(
                _playerAnimationSettings.PlayerStartPosition,
                _playerAnimationSettings.PlayerTargetPosition);
            var startTime = Time.time;

            _playerAnimCancellationTokenSource = new CancellationTokenSource();
            
            try
            {
                while (_playerAnimationSettings.Player.transform.position != _playerAnimationSettings.PlayerTargetPosition)
                {
                    // Отключаем возможность перемещаться во время анимации
                    _playerInputController.enabled = false;
                    _playerSpriteController.SetPlayerMovingSprite();
                
                    // Рассчитываем пройденное расстояние
                    var distanceCovered = (Time.time - startTime) * _playerAnimationSettings.PlayerMoveInSpeed;
                    var fractionOfJourney = distanceCovered / journeyLength;

                    // Плавное перемещение с использованием Lerp
                    _playerAnimationSettings.Player.transform.position = Vector2.Lerp(
                        _playerAnimationSettings.PlayerStartPosition,
                        _playerAnimationSettings.PlayerTargetPosition,
                        fractionOfJourney);

                    // Если достигли цели, выходим
                    if (fractionOfJourney >= 1f)
                    {
                        _playerAnimationSettings.Player.transform.position = _playerAnimationSettings.PlayerTargetPosition;
                        _playerInputController.enabled = true;
                        _playerSpriteController.SetPlayerIdleSprite();
                        break;
                    }

                    // Ждем один кадр
                    await UniTask.Yield(PlayerLoopTiming.Update, _playerAnimCancellationTokenSource.Token);

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
            if (_earthAnimCancellationTokenSource == null) return;
            
            try
            {
                if (!_earthAnimCancellationTokenSource.IsCancellationRequested)
                    _earthAnimCancellationTokenSource.Cancel();
                _earthAnimCancellationTokenSource.Dispose();
                
                if (!_playerAnimCancellationTokenSource.IsCancellationRequested)
                    _playerAnimCancellationTokenSource.Cancel();
                _playerAnimCancellationTokenSource.Dispose();
                
            }
            catch (ObjectDisposedException) {}
            
            _earthAnimCancellationTokenSource = null;
            _playerAnimCancellationTokenSource = null;
        }
        
    }
}