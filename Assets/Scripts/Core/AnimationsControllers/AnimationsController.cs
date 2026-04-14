using System;
using System.Threading;
using Core.AnimationsSettings;
using Core.PlayerLogic;
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
        private PlayerAnimationSettings _playerAnimationSettings;
        private PlayerSpriteController _playerSpriteController;
        private PlayerInputController _playerInputController;
        private readonly ParticleSystem _space;
        private SignalBus _signalBus;
        
        private CancellationTokenSource _earthAnimCancellationTokenSource;
        private CancellationTokenSource _playerAnimCancellationTokenSource;

        [Inject]
        private void Construct(
            EarthAnimationSettings earthAnimationSettings,
            PlayerAnimationSettings playerAnimationSettings,
            PlayerSpriteController playerSpriteController,
            PlayerInputController playerInputController,
            SignalBus signalBus)
        {
            _earthAnimationSettings = earthAnimationSettings;
            _playerAnimationSettings = playerAnimationSettings;
            _playerSpriteController = playerSpriteController;
            _playerInputController = playerInputController;
            _signalBus = signalBus;
        }
        
        public AnimationsController(
            EarthAnimationSettings earthAnimationSettings,
            PlayerAnimationSettings playerAnimationSettings, 
            PlayerSpriteController playerSpriteController,
            ParticleSystem space)
        {
            _earthAnimationSettings = earthAnimationSettings;
            _earthAnimationSettings.Earth.position = _earthAnimationSettings.EarthStartPosition;
            _playerAnimationSettings = playerAnimationSettings;
            _playerInputController = _playerAnimationSettings.Player.GetComponent<PlayerInputController>();
            _playerSpriteController = playerSpriteController;
            _space = space;
        }

        public void OnGameStart()
        {
            _earthAnimationSettings.Earth.gameObject.SetActive(true);
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
                        _signalBus.Fire<StartAnimationCompleted>();
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
    }
}