using System;
using System.Collections.Generic;
using System.Collections; //arraylist
using System.Linq;
using System.Text;
using System.Threading;
using SortsBenchmark;

namespace SortsBenchmark
{

    class Program
    {
        const int RANDOM_DATA = 1;
        const int ALMOST_SORTED = 2;
        const int ALMOST_REVERSE_SORTED = 3;
        static void Main(string[] args)
        {
            //defaults
            int arg1 = 1000000;
            int arg2 = Sorts.HIGH_VARIANCE;
            int arg3 = RANDOM_DATA;

            if (args.Length == 0)
            {
                Console.WriteLine("Pokial si prajete zmenit vstupnu sadu, spustite program z konzoly v tvare: SortsBenchmark.exe pocet_cisel_sady najvacsie_cislo_v_sade typ_sady");
                Console.WriteLine("kde typ sady je integer: 1=RANDOM, 2=ALMOST_SORTED, 3=ALMOST_REVERSE_SORTED. Default=1");
            }
            if (args.Length >= 3)
            {
                arg3 = int.Parse(args[2]);
            }
            if (args.Length >= 2)
            {
                arg2 = int.Parse(args[1]);
            }

            if (args.Length >= 1)
            {
                arg1 = int.Parse(args[0]);
            }

            List<int> numbers = GenerateNumbers(arg1, arg2,arg3);
            //List<int> numbers = new List<int>(new int[] { 12345 });

            List<int> it;

            var sw = new System.Diagnostics.Stopwatch();

            ////bubblesort
            //sw.Start();
            //it = Sorts.BubbleSort(new List<int>(numbers));
            //sw.Stop();
            //PrintSWLog(sw, "Bubblesort");


            for (int switchlimit = 10; switchlimit < 80; switchlimit += 15)
            {
                //quicksort
                sw.Restart();
                it = Sorts.QuickSort(new List<int>(numbers), switchlimit);
                sw.Stop();
                PrintSWLog(sw, "QuickSort s uzitim InsertionSort na vstupy o velkosti do " + switchlimit + " cisiel");
            }
            for (int switchlimit = 10; switchlimit < 80; switchlimit += 15)
            {
                //quicksort-iterative
                sw.Restart();
                it = Sorts.IterativeQuickSort(new List<int>(numbers), switchlimit);
                sw.Stop();
                PrintSWLog(sw, "IterativeQuickSort s uzitim InsertionSort na vstupy o velkosti do " + switchlimit + " cisiel");
                //Sorts.PrintArray(it, "vysledok WikiQuickSortu");

            }
            for (int switchlimit = 10; switchlimit < 80; switchlimit += 15)
            {
                //mergesort
                sw.Restart();
                it = Sorts.MergeSort(new List<int>(numbers), switchlimit);
                sw.Stop();
                PrintSWLog(sw, "MergeSort s uzitim InsertionSort na vstupy o velkosti do " + switchlimit + " cisiel");
                //Sorts.PrintArray(it, "vysledok mergesortu");
            }


            //heapsort
            for (int regularity = 2; regularity <= 10; regularity++)
            {
                sw.Restart();
                it = Sorts.HeapSort(new List<int>(numbers), regularity);
                sw.Stop();
                PrintSWLog(sw, "HeapSort s " + regularity + "-regularnou haldou");
            }

            //radixsort 
            for (int prechody = 2; prechody <= 5; prechody++)
            {
                int exp = (int)Math.Ceiling(32 / (prechody * 1.0));
                int zaklad = (int)Math.Pow(2, exp);
                sw.Restart();
                it = Sorts.RadixSort2(new List<int>(numbers), prechody);
                sw.Stop();

                //Sorts.PrintArray(it, "radixsort");
                PrintSWLog(sw, prechody + "-prechodovy RadixSort (radix=2^" + exp + "=" + zaklad + ")");
               
            }


            Console.WriteLine("DONE");
            Console.ReadLine();
        }


        static List<int> GenerateNumbers(int count, int maxval = int.MaxValue, int randomtype=1)
        {
            Console.WriteLine("Algoritmy budu triedit " + count + " cisel v rozsahu 0.." + maxval);

            if (randomtype == 1) // random
            {
                return GenerateRandomNumbers(count, maxval);
            }
            else // almost (reverse)sorted
            {
                List<int> numbers = GenerateRandomNumbers(count, maxval);
                numbers.Sort();

                //randomly exchange 1% of numbers
                for (int i = 0; i < count / 100; i++)
                {
                    Random rand = new Random();
                    int index1 = rand.Next(count - 1);
                    int index2 = rand.Next(count - 1);
                    Sorts.Swap(numbers, index1, index2);
                }
                if (randomtype == 3) // reverse sorted
                {
                    numbers.Reverse();
                }
                return numbers;
            }
        }

        static List<int> GenerateRandomNumbers(int count, int maxval = int.MaxValue)
        {
            List<int> ret = new List<int>();

            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                ret.Add(rand.Next(maxval));
            }
            return ret;
        }

        static void PrintSWLog(System.Diagnostics.Stopwatch sw, string op)
        {
            Console.WriteLine(op + " lasted for " + sw.ElapsedMilliseconds + " milisec");
        }
    }
}