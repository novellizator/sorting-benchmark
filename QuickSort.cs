using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace SortsBenchmark
{
    public static partial class Sorts
    {
        // recursive
        public static List<int> QuickSort(List<int> numbers, int switchlimit = 10)
        {

            if (numbers.Count <= switchlimit)
            {
                //return Sorts.BubbleSort(numbers);
                return Sorts.InsertionSort(numbers);
            }

            // choice of pivotIndex
            int tmpMidIndex = numbers.Count / 2;
            int tmpFirstIndex = 1;
            int tmpLastIndex = numbers.Count - 1;
            int pivot;

            int a = numbers[tmpMidIndex];
            int b = numbers[tmpFirstIndex];
            int c = numbers[tmpLastIndex];
            pivot = getMedian(a, b, c);


            List<int> M = new List<int>();
            List<int> P = new List<int>();
            List<int> V = new List<int>();
            List<int> ret = new List<int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] < pivot)
                {
                    M.Add(numbers[i]);
                }
                else if (numbers[i] == pivot)
                {
                    P.Add(numbers[i]);
                }
                else if (numbers[i] >= pivot)
                {
                    V.Add(numbers[i]);
                }
            }
            M = QuickSort(M, switchlimit);
            V = QuickSort(V, switchlimit);
            ret.AddRange(M);
            ret.AddRange(P);
            ret.AddRange(V);
            return ret;
        }
        private static int getMedian(int a, int b, int c)
        {
            int pivot;
            int small, large;
            if (a >= b)
            {
                large = a;
                small = b;
            }
            else
            {
                large = b;
                small = a;
            }
            if (c >= large)
            {
                pivot = large;
            }
            else
            {
                if (c < small)
                {
                    pivot = small;
                }
                else
                {
                    pivot = c;
                }
            }
            return pivot;
        }



        //in-array
        public static List<int> IterativeQuickSort(List<int> numbers, int switchlimit = 10)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(0);
            stack.Push(numbers.Count-1);

            while (stack.Count > 0)
            {
                int right = stack.Pop();
                int left = stack.Pop();

                if (left < right)
                {
                    if (right-left < switchlimit)
                    {
                        InsertionSort(numbers, left, right+1);
                        continue;
                    }

                    // choice of pivotIndex
                    int tmpMidIndex = (left+right)/2;
                    int tmpFirstIndex = left;
                    int tmpLastIndex = right;
                    int pivot;

                    int a = numbers[tmpMidIndex];
                    int b = numbers[tmpFirstIndex];
                    int c = numbers[tmpLastIndex];
                    pivot = getMedian(a, b, c);
                    int pivotIndex;
                    if (pivot == a)
                    {
                        pivotIndex = tmpMidIndex;
                    }
                    else if (pivot==b)
                    {
                        pivotIndex = tmpFirstIndex;
                    } else
	                {
                        pivotIndex = tmpLastIndex;        
	                }


                    int pivotNewIndex = partition(numbers, left, right, pivotIndex);

                    stack.Push(left);
                    stack.Push(pivotNewIndex - 1);

                    stack.Push(pivotNewIndex + 1);
                    stack.Push(right);
                }

            }
            return numbers;
        }
        private static int partition(List<int> arr, int left, int right, int pivotIndex)
        {
            int pivotVal = arr[pivotIndex];

            //inlined :-)
            //Swap(arr, pivotIndex, right);
            int tmp = arr[pivotIndex];
            arr[pivotIndex] = arr[right];
            arr[right] = tmp;

            int storeIndex = left;
            for (int i = left; i < right; i++)
            {
                if (arr[i] < pivotVal)
                {
                    //inlined
                    //Swap(arr, i, storeIndex++);
                    int tmp2 = arr[i];
                    arr[i] = arr[storeIndex];
                    arr[storeIndex] = tmp2;
                    storeIndex++;
                }
            }
            //Swap(arr, storeIndex, right);
            int tmp3 = arr[storeIndex];
            arr[storeIndex] = arr[right];
            arr[right] = tmp3;
            return storeIndex;
        }
    }
}