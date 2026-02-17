using UnityEngine;

namespace Core.ObjectMovers
{
    public interface IMover<T> where T: Component
    {
        public void StartObjectMoving(T item);
    }
}