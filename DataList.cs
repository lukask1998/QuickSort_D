using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickSort_D
{
    abstract class DataList
    {

        protected int length;
        public int Length { get { return length; } }
        public abstract double Head();
        public abstract double Next();
        public abstract double Value(int Nr);
        public abstract void Swap(int a, int b);

        public void Print(int n)
        {
            Console.Write(" {0:F5} ", Head());
            for (int i = 1; i < n; i++)
                Console.Write(" {0:F5} ", Next());
            Console.WriteLine();
        }

    }
}
