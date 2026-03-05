using Core.HealthSystem;
using UnityEngine;

namespace Core.Configs
{
    public class AsteroidsData
    {
        public Transform AsteroidsContainer;
        public Transform[] AsteroidSpawnPositions;
        
        public float LargeAsteroidMovingSpeedX;
        public float LargeAsteroidMovingSpeedY;
        public float LargeAsteroidRotationSpeed;
        public int LargeAsteroidPoolSize;
        
        public float MediumAsteroidMovingSpeedX;
        public float MediumAsteroidMovingSpeedY;
        public float MediumAsteroidRotationSpeed;
        public int MediumAsteroidPoolSize;
        public int MediumAsteroidCount;
        
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