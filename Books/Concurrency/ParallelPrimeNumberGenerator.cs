using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurrency
{
    public class ParallelPrimeNumberGenerator
    {
        public void Generate(int upperLimit)
        {
            Parallel.For(2, upperLimit + 1, i =>
            {
                if (IsPrime(i))
                {
                    Console.WriteLine(i);
                }
            });
        }
        private bool IsPrime(int number)
        {
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
