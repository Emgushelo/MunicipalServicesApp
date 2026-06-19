using System;
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public class PriorityQueue<T, TPriority> where TPriority : IComparable
    {
        private SortedDictionary<TPriority, Queue<T>> _dict = new SortedDictionary<TPriority, Queue<T>>();

        public int Count { get; private set; }

        public void Enqueue(T item, TPriority priority)
        {
            if (!_dict.ContainsKey(priority))
                _dict[priority] = new Queue<T>();

            _dict[priority].Enqueue(item);
            Count++;
        }

        public T Dequeue()
        {
            if (Count == 0) return default(T);

            foreach (var kvp in _dict)
            {
                if (kvp.Value.Count > 0)
                {
                    Count--;
                    return kvp.Value.Dequeue();
                }
            }
            return default(T);
        }

        public T Peek()
        {
            foreach (var kvp in _dict)
            {
                if (kvp.Value.Count > 0)
                    return kvp.Value.Peek();
            }
            return default(T);
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        internal void Clear()
        {
            throw new NotImplementedException();
        }
    }
}