using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    internal class Pair<T1, T2>
    {
        internal T1 First { get; set; }
        internal T2 Second { get; set; }

        internal Pair() { }
        internal Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }

        public void Print()
        {
            Console.WriteLine($"First: {First}, Second: {Second}");
        }
    }
}
