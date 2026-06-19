using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MunicipalServicesApp.DataStructures
{
    /// <summary>
    /// Min Heap implementation - Priority queue with minimum value at root
    /// </summary>
    public class MinHeap<T> where T : IComparable<T>
    {
        private List<T> heap;
        private int capacity;

        public MinHeap(int capacity = 100)
        {
            this.capacity = capacity;
            heap = new List<T>(capacity);
        }

        public int Count => heap.Count;
        public bool IsEmpty => heap.Count == 0;
        public bool IsFull => heap.Count >= capacity;

        // ============================================================
        // INSERT - O(log n)
        // ============================================================
        public void Insert(T item)
        {
            if (IsFull)
                throw new InvalidOperationException("Heap is full");

            heap.Add(item);
            HeapifyUp(heap.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (heap[index].CompareTo(heap[parentIndex]) >= 0)
                    break;

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        // ============================================================
        // EXTRACT MIN - O(log n)
        // ============================================================
        public T ExtractMin()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Heap is empty");

            T root = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (!IsEmpty)
                HeapifyDown(0);

            return root;
        }

        private void HeapifyDown(int index)
        {
            int lastIndex = heap.Count - 1;
            while (index < lastIndex)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int smallest = index;

                if (leftChild <= lastIndex && heap[leftChild].CompareTo(heap[smallest]) < 0)
                    smallest = leftChild;

                if (rightChild <= lastIndex && heap[rightChild].CompareTo(heap[smallest]) < 0)
                    smallest = rightChild;

                if (smallest == index)
                    break;

                Swap(index, smallest);
                index = smallest;
            }
        }

        // ============================================================
        // PEEK - O(1)
        // ============================================================
        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Heap is empty");
            return heap[0];
        }

        // ============================================================
        // HELPERS
        // ============================================================
        private void Swap(int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        public List<T> GetAll()
        {
            return new List<T>(heap);
        }

        public void Clear()
        {
            heap.Clear();
        }

        // ============================================================
        // DISPLAY
        // ============================================================
        public void DisplayHeap(ListView lvDisplay, Func<T, string[]> itemFormatter)
        {
            lvDisplay.Items.Clear();
            foreach (var item in heap)
            {
                var values = itemFormatter(item);
                var listItem = new ListViewItem(values[0]);
                for (int i = 1; i < values.Length; i++)
                {
                    listItem.SubItems.Add(values[i]);
                }
                lvDisplay.Items.Add(listItem);
            }
        }

        public string GetHeapStructure()
        {
            if (IsEmpty)
                return "Heap is empty";

            return GetHeapStructureRec(0, 0);
        }

        private string GetHeapStructureRec(int index, int level)
        {
            if (index >= heap.Count)
                return "";

            string indent = new string(' ', level * 4);
            string result = indent + heap[index].ToString() + "\n";

            int left = 2 * index + 1;
            int right = 2 * index + 2;

            if (left < heap.Count)
                result += GetHeapStructureRec(left, level + 1);
            if (right < heap.Count)
                result += GetHeapStructureRec(right, level + 1);

            return result;
        }

        public bool IsValidHeap()
        {
            return IsValidHeapRec(0);
        }

        private bool IsValidHeapRec(int index)
        {
            if (index >= heap.Count)
                return true;

            int left = 2 * index + 1;
            int right = 2 * index + 2;

            if (left < heap.Count && heap[index].CompareTo(heap[left]) > 0)
                return false;

            if (right < heap.Count && heap[index].CompareTo(heap[right]) > 0)
                return false;

            return IsValidHeapRec(left) && IsValidHeapRec(right);
        }
    }
}