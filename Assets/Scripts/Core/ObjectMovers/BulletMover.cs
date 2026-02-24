using Core.Configs;
using Core.ObjectPools;
using Core.ProjectilesPresentation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.ObjectMovers
{
    public class BulletMover : IMover<Bullet>
    {
    private readonly float _bulletSpeed;
    private readonly ObjectPool<Bullet> _magazine;
    
    public BulletMover(
        ProjectilesData projectilesData, ObjectPool<Bullet> magazine)
    {
        _bulletSpeed = projectilesData.BulletSpeed;
        _magazine = magazine;
    }
    
    public void StartObjectMoving(Bullet item)
    {
        _ = MoveBullet(item);
    }
    
    private async UniTask MoveBullet(Bullet bullet)
    {
        while (bullet && bullet.isActiveAndEnabled && Application.isPlaying)
        {
            if (!bullet) break;
            bullet.transform.Translate(
                bullet.transform.up * (_bulletSpeed * Time.deltaTime), 
                Space.World);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }
    
     }
}