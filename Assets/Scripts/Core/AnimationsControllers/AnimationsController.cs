using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.AnimationsControllers
{
    public class AnimationsController
    {
        private EarthAnimationSettings _earthAnimationSettings;
        private PlayerAnimationSettings _playerAnimationSettings;
        
        public AnimationsController(
            EarthAnimationSettings earthAnimationSettings, 
            PlayerAnimationSettings playerAnimationSettings)
        {
            _earthAnimationSettings = earthAnimationSettings;
            _earthAnimationSettings.Earth.position = _earthAnimationSettings.EarthStartPosition;
            _playerAnimationSettings = playerAnimationSettings;
        }

        public void OnGameStart()
        {
            _ = MoveOutEarth();
        }
        
        private async UniTask MoveOutEarth()
        {
            var journeyLength = Vector3.Distance(
                _earthAnimationSettings.EarthStartPosition, 
                _earthAnimationSettings.EarthTargetPosition);
            var startTime = Time.time;
        
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
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }
        
        public void SetPlayerIdleSprite(ref SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = _playerAnimationSettings.PlayerIdleSprite;
        }

        public void SetPlayerMovingSprite(ref SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = _playerAnimationSettings.PlayerMovingSprite;
        }

        public void SetPlayerRollLeftSprite(ref SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = _playerAnimationSettings.PlayerRollLeftSprite;
        }

        public void SetPlayerRollRightSprite(ref SpriteRenderer spriteRenderer)
        {
            spriteRenderer.sprite = _playerAnimationSettings.PlayerRollRightSprite;
        }
        
    }
}