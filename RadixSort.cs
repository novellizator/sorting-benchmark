using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace SortsBenchmark
{
    public static partial class Sorts
    {



        public static List<int> RadixSort2(List<int> numbers, int Digits)
        {
            int exp = (int)Math.Ceiling(32 / (Digits * 1.0));
            int radix = (int)Math.Pow(2, exp);

            List<int> Out = Enumerable.Repeat(0, numbers.Count).ToList();
            List<int> index = Enumerable.Repeat(0, radix).ToList();
            List<int> tmp;
            int Digit = Digits - 1;
            while (Digit >= 0)
            {
                List<int> count = Enumerable.Repeat(0, radix).ToList();

                foreach (var i in numbers)
                {
                    //konkretna cifra nejakeho cisla
                    int cifra = i >> ((Digits - Digit - 1) * exp) & ((1 << exp) - 1);
                    count[cifra]++;
                }

                index[0] = 0;
                for (int i = 1; i < radix; i++)
                {
                    index[i] = index[i - 1] + count[i - 1];
                }

                for (int i = 0; i < numbers.Count; i++)
                {
                    int cifra = numbers[i] >> ((Digits - Digit - 1) * exp) & ((1 << exp) - 1);
                    Out[index[cifra]] = numbers[i];
                    index[cifra]++;
                }

                //swap
                tmp = numbers;
                numbers = Out;
                Out = tmp;

                Digit--;
            }
            return numbers;
        }

        public static List<int> RadixSort3(List<int> numbers, int Digits)
        {
            //marny pokus o maximalnu efektivitu - kontraproduktivny
            int exp = (int)Math.Ceiling(32 / (Digits * 1.0));
            int radix = (int)Math.Pow(2, exp);

            List<int> Out = Enumerable.Repeat(0, numbers.Count).ToList();
            List<int> index = Enumerable.Repeat(0, radix).ToList();
            List<int> count = Enumerable.Repeat(0, radix).ToList();
            List<int> freshCount = Enumerable.Repeat(0, radix).ToList();
            List<int> tmp;
            int Digit = Digits - 1;
            while (Digit >= 0)
            {
                foreach (var i in numbers)
                {
                    //konkretna cifra nejakeho cisla
                    int cifra = i >> ((Digits - Digit - 1) * exp) & ((1 << exp) - 1);

                    if (freshCount[cifra] != Digit)
                    {
                        count[cifra] = 0;
                        freshCount[cifra] = Digit;
                    }
                    count[cifra]++;
                }

                index[0] = 0;
                for (int i = 1; i < radix; i++)
                {
                    index[i] = index[i - 1] + count[i - 1];
                }

                for(int i = 0; i < numbers.Count; i++)
                {
                    int cifra = numbers[i] >> ((Digits - Digit - 1) * exp) & ((1 << exp) - 1);
                    Out[index[cifra]] = numbers[i];
                    index[cifra]++;
                }

                //swap
                tmp = numbers;
                numbers = Out;
                Out = tmp;

                Digit--;
            }
            return numbers;
        }

        public static List<int> RadixSort(List<int> numbers, int Digits = 10)
        {
            int exp = (int)Math.Ceiling(32 / (Digits * 1.0));
            int radix = (int)Math.Pow(2, exp);

            int Digit = Digits - 1;
            while (Digit >= 0)
            {
                List<List<int>> buckets = new List<List<int>>();
                for (int i = 0; i < radix; i++)
                {
                    buckets.Add(new List<int>());
                }

                foreach (int i in numbers)
                {

                    //int cifra = nthDigit2(i,Digits-Digit-1, exp); //konkretna cifra nejakeho cisla
                    int cifra = i >> ((Digits - Digit - 1) * exp) & ((1 << exp) - 1);
                    buckets[cifra].Add(i);
                }

                numbers.Clear();
                for (int i = 0; i < radix; i++)
                {
                    foreach (var item in buckets[i])
                    {
                        numbers.Add(item);
                    }
                }

                Digit--;
            }
            return numbers;
        }
        // digits indexed from the least significant=0
        public static int nthDigit(int number, int n, int exp)
        {
            int radix = (int)Math.Pow(2, exp);
            for (int i = 0; i < n; i++)
			{
			    number /= radix;
			}

            return number % radix;
        }
        public static int nthDigit2(int number, int n, int exp = 10)
        {
            return number >> (n * exp) & ((1 << exp) - 1);
        }
    }
    
}