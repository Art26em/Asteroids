using Core.Entities.Player.Fighting.Projectiles;
using Core.ObjectPools;
using Core.Spawners;
using UnityEngine;
using Zenject;

namespace Core.Entities.Player.Fighting.Weapons
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