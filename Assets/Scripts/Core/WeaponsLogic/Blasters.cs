using Core.ProjectilesPresentation;
using Core.Spawners;

namespace Core.WeaponsLogic
{
    public class Blasters
    {
    private readonly ObjectSpawner<Bullet> _spawner;
    
    public Blasters(ObjectSpawner<Bullet> spawner)
    {
        _spawner = spawner;
    }
    
    public void Shoot()
    {
        _spawner.StartObjectsSpawning(false);
    }
    
    }
}