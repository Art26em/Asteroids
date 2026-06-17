using Core.ObjectSpawners;
using Core.ProjectilesPresentation;
using UnityEngine;
using Zenject;

namespace Core.ProjectilesLogic
{
    public class BulletsProvider
    {
        private ObjectSpawner<Bullet> _objectSpawner;

        [Inject]
        private void Construct(ObjectSpawner<Bullet> objectSpawner)
        {
            _objectSpawner = objectSpawner;
        }
        
        public bool TryGetBullet(Transform spawnPoint, out Bullet bullet)
        {
            return _objectSpawner.TrySpawnObject(spawnPoint, out bullet);
        }
    }
}