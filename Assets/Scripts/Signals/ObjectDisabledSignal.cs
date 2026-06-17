using UnityEngine;

namespace Signals
{
    public struct ObjectDisabledSignal
    {
        public GameObject GameObject;

        public ObjectDisabledSignal(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}