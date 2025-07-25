using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methods
{
    internal class Circle
    {
        public delegate void CircleDelegate(double radius);

        public void Area(double radius)
        {
            Console.WriteLine($"Circle area is {radius * radius * Math.PI}");
        }

        public void Perimeter(double radius)
        {
            Console.WriteLine($"Circle perimeter is {2 * radius * Math.PI}");
        }

        public void ShowInfo(double radius)
        {
            Console.WriteLine($"Circle radius is {radius}");
        }

    }
}
