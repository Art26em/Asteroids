using Core.HealthSystem;
using Core.SpeedSystem;
using UnityEngine;

namespace Core.Configs
{
    public class EnemiesData
    {
        public Transform EnemiesContainer; 
        public Transform[] EnemiesSpawnPoints; 
        public HealthStats LightEnemyHealthStats;
        public SpeedStats LightEnemySpeedStats;
        public int LightEnemyPoolSize;
        public int MediumEnemyPoolSize;
        public int LightEnemyTimeToSpawn;
        public int Damage;
        
        public void SetContainer(Transform container)
        {
            EnemiesContainer = container;
        }

        public void SetSpawnPositions(Transform[] spawnPositions)
        {
            EnemiesSpawnPoints = spawnPositions;
        }
        
    }
}