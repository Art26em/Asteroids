using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.Views
{
    [Serializable]
    public class CollectionView<T> : IEnumerable<T> where T : Component
    {
        public int Count => _items.Count;
        
        [SerializeField] private T itemPrefab;
        [SerializeField] private Transform container;
        
        private readonly List<T> _items = new();

        public T AddItem()
        {
            T item = Object.Instantiate(itemPrefab, container);
            _items.Add(item);
            return item;
        }

        public void RemoveItem(T item)
        {
            if (_items.Remove(item))
            {
                Object.Destroy(item.gameObject);
            }
        }
        
        public void Clear()
        {
            foreach (var item in _items)
            {
                Object.Destroy(item);
            }
            _items.Clear();
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}