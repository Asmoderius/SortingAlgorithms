using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingCore.DataStructures
{
    public class Heap<T> where T:IComparable
    {
        public T[] Array { get; private set; }
        public int Length { get; private set; }
        public HeapType HeapType { get; private set; }

        private readonly int comparator;

        public Heap(T[] array, HeapType heapType)
        {
            Array = array;
            Length = array.Length;
            HeapType = heapType;
            comparator = (heapType == HeapType.MaxHeap) ? 1 : -1;
            BuildHeap();
        }

        private void BuildHeap()
        {
            for (int i = Length/2; i > 0; i--)
            {
                Heapify(i);
            }
        }

        private void Heapify(int index)
        {
            var left = 2 * index;
            var right = 2 * index + 1;
            var parent = index;

            if (left <= Length && Array[left - 1].CompareTo(Array[parent - 1]) == comparator)
            {
                parent = left;
            }
            if(right <= Length && Array[right - 1].CompareTo(Array[parent - 1]) == comparator)
            {
                parent = right;
            }

            if(parent != index)
            {
                Sort<T>.Swap(Array, parent - 1, index - 1);
                Heapify(parent);
            }
        }

        public T RemoveRoot()
        {
            T root = Array[0];
            Array[0] = Array[Length - 1];
            Length--;
            Heapify(1);
            return root;
        }


    }

    public enum HeapType
    {
        MinHeap,
        MaxHeap
    }

}
