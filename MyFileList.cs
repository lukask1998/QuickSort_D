using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Diagnostics;

namespace QuickSort_D
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            // Antras etapas            
            int n = 12;
            string filename;

            filename = @"mydataarray.dat";
            MyFileArray myfilearray = new MyFileArray(filename, n, seed);

            using (myfilearray.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE ARRAY \n");
                myfilearray.Print(n);
                QuickSort(myfilearray, 0, n - 1);
                myfilearray.Print(n);
            }

            filename = @"mydatalist.dat";
            MyFileList myfilelist = new MyFileList(filename, n, seed);

            using (myfilelist.fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite))
            {
                Console.WriteLine("\n FILE LIST \n");
                myfilelist.Print(n);
                QuickSort(myfilelist, 0, n - 1);
                myfilelist.Print(n);
            }

            SpeedTest();
        }

        private static void SpeedTest()
        {
            Stopwatch stopWatch = new Stopwatch();

            string filename1 = @"mydataarray.dat";
            string filename2 = @"mydatalist.dat";

            MyFileArray myArray;
            MyFileList myList;

            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;

            int[] numbers = { 100, 200, 400, 800, 1600, 3200, 6400 };

            Console.WriteLine("EXTERNAL MEMORY ARRAY QUICK SORT");
            Console.WriteLine("|---------------------------|");
            Console.WriteLine("|  N          |  Run time   |");
            Console.WriteLine("|---------------------------|");
            foreach (int number in numbers)
            {
                //speed test for array
                myArray = new MyFileArray(filename1, number, seed);
                using (myArray.fs = new FileStream(filename1, FileMode.Open, FileAccess.ReadWrite))
                {

                    stopWatch.Start();
                    QuickSort(myArray, 0, number - 1);
                    stopWatch.Stop();

                }

                Console.WriteLine("|  {0,-9}  |  {1,2}:{2,2}:{3,3}  |", number,
                    stopWatch.Elapsed.Minutes, stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds);

                //reseting stop watch
                stopWatch.Reset();
            }
            Console.WriteLine("|---------------------------|\n");

            Console.WriteLine("EXTERNAL MEMORY LIST QUICK SORT");
            Console.WriteLine("|---------------------------|");
            Console.WriteLine("|  N          |  Run time   |");
            Console.WriteLine("|---------------------------|");
            foreach (int number in numbers)
            {
                //speed test for array
                myList = null;
                myList = new MyFileList(filename2, number, seed);
                using (myList.fs = new FileStream(filename2, FileMode.Open, FileAccess.ReadWrite))
                {

                    stopWatch.Start();
                    QuickSort(myList, 0, number - 1);
                    stopWatch.Stop();

                }

                Console.WriteLine("|  {0,-9}  |  {1,2}:{2,2}:{3,3}  |", number,
                    stopWatch.Elapsed.Minutes, stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds);

                //reseting stop watch
                stopWatch.Reset();
            }
            Console.WriteLine("|---------------------------|");
        }

        /* The main function that implements QuickSort() 
         * arr[] --> Array to be sorted, 
         * low --> Starting index, 
         * high --> Ending index */
        static void QuickSort(DataArray arr, int low, int high)
        {
            if (low < high)
            {

                /* pi is partitioning index, arr[pi] is  
                now at right place */
                int pi = partition(arr, low, high);

                // Recursively sort elements before 
                // partition and after partition 
                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        /* This function takes last element as pivot, 
         * places the pivot element at its correct 
         * position in sorted array, and places all 
         * smaller (smaller than pivot) to left of 
         * pivot and all greater elements to right 
         * of pivot */
        static int partition(DataArray arr, int low, int high)
        {
            double pivot = arr[high];

            // index of smaller element 
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                // If current element is smaller  
                // than or equal to pivot 
                if (arr[j] <= pivot)
                {
                    i++;

                    // swap arr[i] and arr[j] 
                    arr.Swap(i, j, arr[i], arr[high]);
                }
            }

            // swap arr[i+1] and arr[high] (or pivot) 
            arr.Swap(i + 1, high, arr[low], arr[high]);

            return i + 1;
        }

        /* The main function that implements QuickSort() 
         * arr[] --> Array to be sorted, 
         * low --> Starting index, 
         * high --> Ending index */
        static void QuickSort(DataList arr, int low, int high)
        {
            if (low < high)
            {

                /* pi is partitioning index, arr[pi] is  
                now at right place */
                int pi = partition(arr, low, high);

                // Recursively sort elements before 
                // partition and after partition 
                QuickSort(arr, low, pi - 1);
                QuickSort(arr, pi + 1, high);
            }
        }

        /* This function takes last element as pivot, 
         * places the pivot element at its correct 
         * position in sorted array, and places all 
         * smaller (smaller than pivot) to left of 
         * pivot and all greater elements to right 
         * of pivot */
        static int partition(DataList arr, int low, int high)
        {
            double pivot = arr.Value(high);

            // index of smaller element 
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                // If current element is smaller  
                // than or equal to pivot 
                if (arr.Value(j) <= pivot)
                {
                    i++;

                    // swap arr[i] and arr[j] 
                    arr.Swap(i, j);
                }
            }

            // swap arr[i+1] and arr[high] (or pivot) 
            arr.Swap(i + 1, high);

            return i + 1;
        }

    }
}
