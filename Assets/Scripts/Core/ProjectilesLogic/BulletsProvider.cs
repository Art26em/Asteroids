using System.Collections.Generic;
using Core.Configs;
using Core.ObjectSpawners;
using Core.ProjectilesPresentation;
using Zenject;

namespace Core.ProjectilesLogic
{
    public class BulletsProvider
    {
        private ObjectSpawner<Bullet> _objectSpawner;
        private ProjectilesData _projectilesData;

        [Inject]
        private void Construct(ObjectSpawner<Bullet> objectSpawner, ProjectilesData projectilesData)
        {
            _objectSpawner = objectSpawner;
            _projectilesData = projectilesData;
        }
        
        public List<Bullet> GetBullets()
        {
            var bullets = new List<Bullet>();
            foreach (var spawnPoint in _projectilesData.BulletsShootPoints)
            {
                if (_objectSpawner.TrySpawnObject(spawnPoint, out var bullet))
                {
                    bullets.Add(bullet);
                }     
            }
            return bullets;
        }

        
    }
}