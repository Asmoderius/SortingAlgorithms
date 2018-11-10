using System;
using SortingCore;
using System.Collections;
using System.Diagnostics;
namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {

            TestBubbleSort();
            TestQuickSort();
            TestHeapSort();



            //int[] sorted = Sort<int>.BogoSort(new int[] { 6,2,7,4,3,1,5,8,9,0,10 });

            Console.ReadLine();
        }


        static void TestBubbleSort()
        {
            Stopwatch stopwatch = new Stopwatch();
            long totalTime = 0L;
            for (int i = 0; i < 1; i++)
            {
                stopwatch.Start();
                int[] sortedBubble = Sort<int>.BubbleSort(CreateRandomArray(1000000));
                stopwatch.Stop();
                totalTime += stopwatch.ElapsedMilliseconds;
                stopwatch.Reset();
            }
            Console.WriteLine($"Average time BubbleSort: {totalTime / 10L}");
  
        }

        static void TestQuickSort()
        {
            Stopwatch stopwatch = new Stopwatch();
            long totalTime = 0L;
            for (int i = 0; i < 1; i++)
            {
                stopwatch.Start();
                int[] sortedBubble = Sort<int>.QuickSort(CreateRandomArray(1000000));
                stopwatch.Stop();
                totalTime += stopwatch.ElapsedMilliseconds;
                stopwatch.Reset();
            }
            Console.WriteLine($"Average time QuickSort: {totalTime / 10L}");
        }

        static void TestHeapSort()
        {
            Stopwatch stopwatch = new Stopwatch();
            long totalTime = 0L;
            for (int i = 0; i < 1; i++)
            {
                stopwatch.Start();
                int[] sortedBubble = Sort<int>.HeapSort(CreateRandomArray(1000000));
                stopwatch.Stop();
                totalTime += stopwatch.ElapsedMilliseconds;
                stopwatch.Reset();
            }
            Console.WriteLine($"Average time HeapSort: {totalTime / 10L}");
        }

        static void PrintSorted<T>(T[] array)
        {
            foreach (var element in array)
            {
                Console.Write($"{element.ToString()} ");
            }
            Console.WriteLine();

        }

        static int[] CreateRandomArray(int k)
        {
            int[] array = new int[k];
            Random random = new Random();

            for (int i = 0; i < k; i++)
            {
                array[i] = random.Next();
            }
            return array;
            
        }
    }
}
