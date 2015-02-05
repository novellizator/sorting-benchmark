using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace SortsBenchmark
{
    public static partial class Sorts
    {
        public static List<int> MergeSort(List<int> numbers, int switchlimit = 5)
        {

            //if (numbers.Count <= 1)
            //{
            //    return numbers;
            //}

            if (numbers.Count <= switchlimit)
            {
                return Sorts.BubbleSort(numbers);
            }
            List<int> ret = new List<int>();
            int middle = numbers.Count / 2;


            List<int> L = numbers.GetRange(0, middle);
            List<int> R = numbers.GetRange(middle, numbers.Count - middle);
            L = MergeSort(L, switchlimit);
            R = MergeSort(R, switchlimit);

            return Merge(L, R);
        }

        private static List<int> Merge(List<int> L, List<int> R)
        {

            List<int> ret = new List<int>();

            int l = 0;
            int r = 0;
            for (int i = 0; i < L.Count + R.Count; i++)
            {
                if (l == L.Count)
                {
                    ret.Add(R[r++]);
                }
                else if (r == R.Count)
                {
                    ret.Add(L[l++]);
                }

                else if ((int)L[l] < (int)R[r])
                {
                    ret.Add(L[l++]);
                }
                else
                {
                    ret.Add(R[r++]);
                }
            }

            return ret;
        }

    }
}