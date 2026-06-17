using System;
using System.Threading;
using Core.ObjectMovers;
using Core.PlayerPresentation;
using Core.SpriteControllers;
using Core.States;
using Core.WeaponsLogic;
using Cysharp.Threading.Tasks;
using Signals;
using UnityEngine;
using Zenject;

namespace Core.PlayerLogic
{
    public class PlayerInputController : IInitializable, IDisposable
    {
        private PlayerInputData _playerInput = new();
        private IPlayerInputProvider _inputProvider;
        private PlayerObject _playerObject;
        private PlayerMover _playerMover;
        private PlayerSpriteController _playerSpriteController;
        private BlasterWeapon _blasterWeapon;
        private LaserWeapon _laserWeapon;
        private SignalBus _signalBus;
        
        private CancellationTokenSource _cancellationTokenSource;
        
        [Inject]
        private void Construct(
            IPlayerInputProvider inputProvider,
            PlayerMover playerMover, 
            PlayerObject playerObject,
            PlayerSpriteController playerSpriteController,
            BlasterWeapon blasterWeapon,
            LaserWeapon laserWeapon,
            SignalBus signalBus)
        {
            _inputProvider = inputProvider;
            _playerMover = playerMover;
            _playerObject = playerObject;
            _playerSpriteController = playerSpriteController;
            _blasterWeapon = blasterWeapon;
            _laserWeapon = laserWeapon;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
            _cancellationTokenSource = new CancellationTokenSource();
            _ = HandleInput();
            _ = CalculateMovement();
        }
        
        private async UniTask HandleInput()
        {
            if (!_playerObject.IsInputEnabled) return;
            
            try
            {
                while (Application.isPlaying)
                {
                    _playerInput = _inputProvider.GetPlayerInput();

                    if (_playerInput.IsMovingY != _playerInput.WasMovingY)
                    {
                        _playerInput.WasMovingY = _playerInput.IsMovingY;
                        _playerSpriteController.UpdatePlayerSprite(_playerObject.IsInputEnabled, _playerInput.IsMovingY);
                    }

                    if (_playerInput.IsBlastersShooting)
                    {
                        _blasterWeapon.Shoot();    
                    }
            
                    if (_playerInput.IsLaserShooting)
                    {
                        _laserWeapon.Shoot();    
                    }    
                    
                    await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                _cancellationTokenSource?.Dispose();
            }
        }

        private async UniTask CalculateMovement()
        {
            if (_playerInput == null) return;
            
            try
            {
                while (Application.isPlaying)
                {
                    _playerMover.CalculateVelocity(_playerInput.MovementInput);
                    _playerMover.CalculateRotating(_playerInput.RotationInput);
                    await UniTask.Yield(PlayerLoopTiming.FixedUpdate, _cancellationTokenSource.Token);
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                _cancellationTokenSource?.Dispose();
            }
        }

        private void OnGameStateChanged(GameStateChangedSignal signal)
        {
            if (signal.NewGameState == GameState.GameOver)
            { 
                _cancellationTokenSource?.Cancel();    
            }
        }
        
        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
            _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
        }
    }
}