using UnityEngine;

namespace Core.Configs
{
    public class ProjectilesData
    {
        public int BulletSpeed;
        public int BulletDamage;
        public int MagazineSize;
        public Transform BulletsContainer;
        public Transform[] BulletsShootPoints;
        public float LaserFireTime;
        public float LaserReloadTime;
        
        public void SetContainer(Transform container)
        {
            BulletsContainer = container;
        }

        public void SetSpawnPositions(Transform[] spawnPositions)
        {
            BulletsShootPoints = spawnPositions;
        }
    }
}