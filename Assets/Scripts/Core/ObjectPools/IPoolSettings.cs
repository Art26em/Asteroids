using UnityEngine;

namespace Core.ObjectFactories
{
    public interface IPoolSettings
    {
        struct Settings
        {
            public int Count;
            public Transform Container;
        }

        public Settings GetSettings<T>() where T : new();

    }
}