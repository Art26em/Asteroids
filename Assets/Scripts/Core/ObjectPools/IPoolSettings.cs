using UnityEngine;

namespace Core.ObjectPools
{
    public interface IPoolSettings
    {
        struct Settings
        {
            public int Count;
            public Transform Container;
        }

        public Settings GetSettings<T>();
    }
}