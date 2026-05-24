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
        public int LargeAsteroidScore;
        
        public SpeedStats MediumAsteroidSpeedStats;
        public int MediumAsteroidPoolSize;
        public int MediumAsteroidCount;
        public int MediumAsteroidScore;

        public float MediumAsteroidSpawnDelay;
        public float TimeToSpawn;
        
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