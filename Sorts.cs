using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace SortsBenchmark
{
    public static partial class Sorts
    {
        public static int LOW_VARIANCE = 50;
        public static int MIDDLE_VARIANCE = 5000;
        public static int HIGH_VARIANCE = int.MaxValue;


        public static List<int> BubbleSort(List<int> numbers)
        {

            bool sorted = true;
            for (int i = 0; i < numbers.Count; i++)
            {

                for (int j = 1; j < numbers.Count; j++)
                {
                    if ((int)numbers[j - 1] > (int)numbers[j])
                    {
                        int tmp = (int)numbers[j - 1];
                        numbers[j - 1] = numbers[j];
                        numbers[j] = tmp;
                        sorted = false;
                    }
                }
                if (sorted)
                {
                    return numbers;
                }
            }
            return numbers;
        }

        public static List<int> InsertionSort(List<int> numbers, int from=-1, int to=-1)
        {
            //to - exclusive [from, to)
            if (from == -1)
            {
                from = 1;
                to = numbers.Count;
            }
            for (int i = from+1; i < to; i++)
            {
                int val = numbers[i];
                int prevIndex = i - 1;
                bool finish = false;

                while (!finish)
                {
                    if (numbers[prevIndex] > val)
                    {
                        numbers[prevIndex + 1] = numbers[prevIndex];
                        prevIndex--;
                        if (prevIndex < from)
                        {
                            finish = true;
                        }
                    }
                    else
                    {
                        finish = true;
                    }
                }
                numbers[prevIndex + 1] = val;
            }
            return numbers; 
        }


        public static void PrintArray(List<int> arr, string txt = "", int sleep = 0)
        {
            int sucet = 0;
            Console.WriteLine("{1}({0}): ", arr.Count, txt);
            for (int i = 0; i < arr.Count; i++)
            {
                Console.WriteLine(arr[i].ToString().PadLeft(10));
                sucet += arr[i];
            }
            Console.WriteLine();
            Console.WriteLine("sucet je " + sucet);
            System.Threading.Thread.Sleep(sleep);
        }
        public static void Swap(List<int> arr, int index1, int index2)
        {
            int tmp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = tmp;
        }
    }
}
