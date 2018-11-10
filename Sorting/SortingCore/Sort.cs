using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortingCore.DataStructures;
namespace SortingCore
{
    /*
     * TODO: Code cleanup : Move Comparator variable to global. It will be set in all start methods. It is rather cumbersome to carry it around in parameters.
     * */
    public static class Sort<T> where T : IComparable
    {
        public static T[] BubbleSort(T[] array, bool ascending = true)
        {
            if (IsSorted(array, ascending)) return array;

            var comparator = (ascending) ? -1 : 1;
            bool swap = true;
            while(swap)
            {
                swap = false;
                for (int i = 1; i < array.Length; i++)
                {
                    if(array[i].CompareTo(array[i-1]) == comparator)
                    {
                        Swap(array, i-1,i);
                        swap = true;
                    }
                }
            }
            return array;
        }

        public static T[] InsertionSort(T[] array, bool ascending = true)
        {
            if (IsSorted(array, ascending)) return array;
            var comparator = (ascending) ? -1 : 1;
            var i = 1;
            while(i < array.Length)
            {
                var j = i;
                while(j > 0 && array[j].CompareTo(array[j-1]) == comparator)
                {
                    Swap(array, j - 1, j);
                    j--;
                }
                i++;
            }
            return array;
        }

        public static T[] SelectionSort(T[] array, bool ascending = true)
        {
            if (IsSorted(array, ascending)) return array;
            var comparator = (ascending) ? -1 : 1;
            for (int i = 0; i < array.Length-1; i++)
            {
                int min = i;
                for (int j = i; j < array.Length; j++)
                {
                    if(array[j].CompareTo(array[min]) == comparator)
                    {
                        min = j;
                    }
                }
                if(min != i) Swap(array, min, i);
            }
            return array;
        }

        #region Merge Sort
        public static T[] MergeSort(T[] array, bool useLists, bool ascending = true)
        {
            if (IsSorted(array, ascending)) return array;
            int comparator = (ascending) ? -1 : 1;
            if(!useLists)
            {
                T[] workArray = new T[array.Length];

                CopyArray(array, workArray);
                MergeSortSplit(workArray, 0, array.Length, array, comparator);
            }
            else
            {
                array = MergeSortSplit_List(array.ToList<T>(), comparator).ToArray<T>();
            }

            return array;
        }

        #region Merge Sort with indices
        private static void MergeSortSplit(T[] workArray, int start, int end, T[] array, int comparator)
        {
            if (end - start < 2) return;
            int middle = (end + start) / 2;
            MergeSortSplit(array, start, middle, workArray, comparator);
            MergeSortSplit(array, middle, end, workArray, comparator);
            Merge(workArray, start, middle, end, array, comparator);

        }

        private static void Merge(T[] workArray, int start, int middle, int end, T[] array, int comparator)
        {
            int i = start;
            int j = middle;
            for (int k = start; k < end; k++)
            {
                if(i < middle && (j >= end || workArray[i].CompareTo(workArray[j]) == comparator || workArray[i].CompareTo(workArray[j]) == 0))
                {
                    array[k] = workArray[i];
                    i++;
                }
                else
                {
                    array[k] = workArray[j];
                    j++;
                }
            }
        }

        private static void CopyArray(T[] array, T[] workArray)
        {
            for (int i = 0; i < array.Length; i++)
            {
                workArray[i] = array[i];
            }
        }
        #endregion
        #region Merge Sort with lists
        public static List<T> MergeSortSplit_List(List<T> list, int comparator)
        {
            if (list.Count <= 1) return list;
            var left = new List<T>();
            var right = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                if(i < list.Count/2)
                {
                    left.Add(list[i]);
                }
                else
                {
                    right.Add(list[i]);
                }
            }
            left = MergeSortSplit_List(left, comparator);
            right = MergeSortSplit_List(right, comparator);

            return Merge_List(left, right, comparator);
        }

        private static List<T> Merge_List(List<T> left, List<T> right, int comparator)
        {
            var result = new List<T>();
            int i = 0;
            int j = 0;

            while(i < left.Count && j < right.Count)
            {
                if(left[i].CompareTo(right[j]) == comparator || left[i].CompareTo(right[j]) == 0)
                {
                    result.Add(left[i]);
                    i++;
                }
                else
                {
                    result.Add(right[j]);
                    j++;
                }
            }

            while(i < left.Count)
            {
                result.Add(left[i]);
                i++;
            }
            while(j < right.Count)
            {
                result.Add(right[j]);
                j++;
            }
            return result;
        }
        #endregion
        #endregion

        #region Quick Sort
        public static T[] QuickSort(T[] array, bool randomPivot = false, bool ascending = true)
        {
            int comparator = (ascending) ? 1 : -1;
            if(randomPivot)
            {
                RandomizedQuickSort(array, 0, array.Length - 1, comparator);
            }
            else
            {
                QuickSort(array, 0, array.Length - 1, comparator);
            }
            return array;
        }

        private static T[] QuickSort(T[] array,int left, int right, int comparator)
        {
            if(left < right)
            {
                int p = Partition(array, left, right, comparator);
                QuickSort(array, left, p - 1, comparator);
                QuickSort(array, p + 1, right, comparator);

            }
            return array;
        }

        private static T[] RandomizedQuickSort(T[] array,int left, int right, int comparator)
        {
            if (left < right)
            {
                int p = RandomizedPartition(array, left, right, comparator);
                RandomizedQuickSort(array, left, p - 1, comparator);
                RandomizedQuickSort(array, p+1, right, comparator);
            }
            return array;
        }

        private static int RandomizedPartition(T[] array, int left, int right, int comparator)
        {
            Random random = new Random();
            int r = random.Next(left, right);
            T pivot = array[r];
            array[r] = array[right];
            array[right] = pivot;
            return Partition(array, left, right, comparator);
        }

        private static int Partition(T[] array, int left, int right, int comparator)
        {
            T pivot = array[right];
            int i = left;

            for (int j = left; j < right; j++)
            {
                if (pivot.CompareTo(array[j]) == comparator)
                {
                    Swap(array, i, j);
                    i++;
                }
            }
            array[right] = array[i];
            array[i] = pivot;
            return i;
        }

        #endregion

        public static T[] HeapSort(T[] array, bool ascending = true)
        {
            HeapType heapType = (ascending) ? HeapType.MinHeap : HeapType.MaxHeap;
            Heap<T> heap = new Heap<T>(array, heapType);
            T[] sortedArray = new T[array.Length];
            for (int i = 0; i < sortedArray.Length; i++)
            {
                sortedArray[i] = heap.RemoveRoot();
            }
            return sortedArray;
        }

        #region BogoSort

        private static bool BogoWarning()
        {
            return true;
            Console.WriteLine("Warning: There is no guarantee that this algorithm will yield any results (for n>100 - O((n+1)!) within a human lifespan. Are you certain? y/n");
            ConsoleKeyInfo key = Console.ReadKey();
            bool b = (key.KeyChar == 'y') ? true : false;
            Console.Clear();
            return b;
        }

        public static T[] BogoSort(T[] array, bool ascending = true)
        {
            if(BogoWarning())
            {
                if (IsSorted(array, ascending)) return array;
                long attempt = 0; //Long, because this might take... a lot of attempts
                while (!IsSorted(array, ascending))
                {
                    Random random = new Random();
                    for (int i = 0; i < array.Length; i++)
                    {
                        Swap(array, i, random.Next(0, array.Length - 1));
                    }
                    attempt++;
                }
                Console.WriteLine($"It took {attempt} attempts");

            }
            return array;
        }

        #endregion

        #region Helper methods
        internal static void Swap(T[] array, int i, int j)
        {
            T temp = array[j];
            array[j] = array[i];
            array[i] = temp;
        }

        private static bool IsSorted(T[] array, bool ascending)
        {
            int comparator = 0;
            comparator = (ascending) ? -1 : 1;
            bool isSorted = true;
            for (int i = 0; i < array.Length-1; i++)
            {
                isSorted = (array[i].CompareTo(array[i + 1]) == comparator ? true : false);
                if (!isSorted) break;
            }
            return isSorted;

        }
        #endregion
    }
}
