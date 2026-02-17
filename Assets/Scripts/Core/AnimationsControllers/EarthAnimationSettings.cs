using UnityEngine;

namespace Core.AnimationsControllers
{
    public class EarthAnimationSettings
    {
        public float EarthMoveOutSpeed {get; private set;}
        public Transform Earth {get; private set;}
        public Vector3 EarthStartPosition {get; private set;}
        public Vector3 EarthTargetPosition {get; private set;}

        public EarthAnimationSettings(
            Transform earth,
            float earthMoveOutSpeed, 
            Vector3[] earthStartTargetPositions)
        {
            EarthMoveOutSpeed = earthMoveOutSpeed;
            Earth = earth;
            if (earthStartTargetPositions == null || earthStartTargetPositions.Length < 2)
            {
                EarthStartPosition = new Vector3(0, 0, 0);
                EarthTargetPosition = new Vector3(0, 0, 0); 
            }
            else
            {
                EarthStartPosition = earthStartTargetPositions[0];
                EarthTargetPosition = earthStartTargetPositions[1];
            }
        }
    }
}