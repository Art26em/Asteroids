using Core.ProjectilesPresentation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.ObjectMovers
{
    public class BulletMover
    {
        public void StartBulletMoving(Bullet bullet)
        {
            _ = MoveBullet(bullet);
        }
        
        private async UniTask MoveBullet(Bullet bullet)
        {
            while (bullet && bullet.isActiveAndEnabled && Application.isPlaying)
            {
                if (!bullet) break;
                bullet.transform.Translate(
                    bullet.transform.up * (bullet.Speed * Time.deltaTime), 
                    Space.World);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }
        
    }
}