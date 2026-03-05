using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.ObjectPools
{
    public class ObjectPool<T> where T : Component
    {
        private readonly List<T> _pool = new();

        public void Add(T item)
        {
            _pool.Add(item);
        }

        public bool TryGetItem(out T item)
        {
            item = _pool.FirstOrDefault(item => !item.gameObject.activeInHierarchy);
            item?.gameObject.SetActive(true);
            return item;
        }
        
    }
}