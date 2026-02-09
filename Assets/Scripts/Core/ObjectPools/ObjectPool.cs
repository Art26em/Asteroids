using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.ObjectPools
{
    public class ObjectPool<T> where T : Component, new()
    {
        private readonly List<T> _pool;

        public void Add(T item)
        {
            _pool.Add(item);
        }

        public T Get()
        {
            return _pool.FirstOrDefault(item => item.gameObject.activeInHierarchy);
        }

        public void Return(T item)
        {
            item.gameObject.SetActive(false);    
        }
        
    }
}