using Core.AnimationsControllers;
using Core.AnimationsSettings;
using Core.PlayerPresentation;
using Core.SpriteControllers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class AnimationsInstaller : MonoInstaller
    {
        [Header("Player animations settings")]
        [SerializeField] private PlayerObject playerObject;
        [SerializeField] private float playerMoveInSpeed;
        [SerializeField] private Sprite[] playerIdleMovingSprites;
        [SerializeField] private Vector3[] playerStartTargetPositions;
        
        [Header("Space animations settings")]
        [SerializeField] private Transform earth;
        [SerializeField] private float earthMoveOutSpeed;
        [SerializeField] private Vector3[] earthStartTargetPositions;
        [SerializeField] private ParticleSystem space;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<PlayerObject>().FromInstance(playerObject).AsSingle();
            Container.Bind<PlayerSpriteController>().AsSingle().WithArguments(playerIdleMovingSprites);
            Container.Bind<EarthAnimationSettings>().AsSingle().WithArguments(
                earth, earthMoveOutSpeed, earthStartTargetPositions);
            Container.Bind<PlayerAnimationSettings>().AsSingle().WithArguments(
                playerMoveInSpeed, playerStartTargetPositions);
            Container.Bind<AnimationsController>().AsSingle().WithArguments(space);
        }
        
    }
}