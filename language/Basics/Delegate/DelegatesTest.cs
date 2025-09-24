using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    public class DelegatesTest
    {
        //public void Add(int x, int y = 0)
        //{
        //    Console.WriteLine($"{x} + {y} = {x + y}");
        //}
        // Changed add to a expression bodied method
        public void Add(int x, int y = 0) => Console.WriteLine($"{x} + {y} = {x + y}");
        public void Multiply(int x, int y)
        {
            Console.WriteLine($"{x} * {y} = {x * y}");
        }

    }
}
