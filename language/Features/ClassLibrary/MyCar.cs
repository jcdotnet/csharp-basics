using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class MyCar: ICar
    {
        public void Run()
        {
            Console.WriteLine("My car is running");
        }
    }
}
