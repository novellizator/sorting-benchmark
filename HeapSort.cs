using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace SortsBenchmark
{
    public static partial class Sorts
    {

        public static List<int> HeapSort(List<int> numbers, int regularity = 2)
        {
            // NOTICE: root in the heap is in heap[1] ... heap[0] is unused
            List<int> heap = HeapCreate(numbers, regularity);
            return HeapPops(heap, regularity);
        }
        public static List<int> HeapCreate(List<int> numbers, int regularity = 2)
        {
            // add the "invalid" element to numbers[0]! 
            numbers.Add(0);
            Swap(numbers, 0, numbers.Count - 1);

            for (int i = numbers.Count/2; i >0; i--)
            {
                percolateDown(numbers, regularity, numbers.Count-1, i);                
            }
            return numbers;
        }
        public static List<int> HeapPops(List<int> heap, int regularity = 2)
        {
            List<int> sorted = new List<int>();

            int heapsize = heap.Count - 1; // index of the last valid element in the heap
            for (int i = 0; i < heap.Count - 1; i++)
            {
                int el = heap[1];
                heap[1] = heap[heapsize];
                heapsize = heapsize - 1;

                // position of a root element is 1
                percolateDown(heap, regularity, heapsize, 1);
                
                // place the element at the invalid position
                heap[heapsize + 1] = el; 
            }
            heap.RemoveAt(0);
            heap.Reverse(); // incredibly fast! is that an O(1) ? :-)
            return heap;
        }

        private static void percolateDown(List<int> heap, int regularity, int heapsize, int pos)
        {
            bool end = false;
            while (!end)
            {
                int minIndex = -1; //invalid by default
                int minValue = int.MaxValue;
                //find the child with the smallest value. If it weren't the smallest value - it would make the heap invalid!
                for (int j = (pos - 1) * regularity + 2; j < pos * regularity + 2; j++)
                {
                    if (j <= heapsize && heap[j] < (int)heap[pos])
                    {
                        if (heap[j] < minValue)
                        {
                            minValue = heap[j];
                            minIndex = j;
                        }
                    }
                }
                //swap myself with the smallest element
                if (minIndex != -1)
                {
                    //inlined swap
                    //Swap(heap, pos, minIndex);
                    int tmp = heap[pos];
                    heap[pos] = heap[minIndex];
                    heap[minIndex] = tmp;

                    pos = minIndex;
                }
                else
                {
                    end = true;
                }
            }
        }

    }
}