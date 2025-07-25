using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    internal class Ferrari : ICar
    {
        public void Run()
        {
            Console.WriteLine("Car is running");
        }
    }
}
