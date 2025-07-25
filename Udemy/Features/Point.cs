using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        //public Point(int x, int y) => (X, Y) = (x, y);

        public void GetCoords( out int x, out int y)
        {
            x = X; y = Y;
        }

        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
    }
}
