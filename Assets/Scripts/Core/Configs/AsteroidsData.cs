using Core.HealthSystem;
using Core.SpeedSystem;
using UnityEngine;

namespace Core.Configs
{
    public class AsteroidsData
    {
        public Transform AsteroidsContainer;
        public Transform[] AsteroidSpawnPositions;
        
        public SpeedStats LargeAsteroidSpeedStats;
        public int LargeAsteroidPoolSize;
        
        public SpeedStats MediumAsteroidSpeedStats;
        public int MediumAsteroidPoolSize;
        public int MediumAsteroidCount;

        public float MediumAsteroidSpawnDelay;
        public float TimeToSpawn;
        public int Damage;
        public HealthStats HealthStats;
        
        public void SetContainer(Transform asteroidsContainer)
        {
            AsteroidsContainer = asteroidsContainer;
        }

        public void SetSpawnPositions(Transform[] asteroidsSpawnPositions)
        {
            AsteroidSpawnPositions = asteroidsSpawnPositions;
        }
        
    }
}