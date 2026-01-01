using UnityEngine;

namespace Core.AnimationsSettings
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
            Vector3 earthStartPosition, 
            Vector3 earthTargetPosition)
        {
            EarthMoveOutSpeed = earthMoveOutSpeed;
            Earth = earth;
            EarthStartPosition = earthStartPosition;
            EarthTargetPosition = earthTargetPosition;
        }
    }
}