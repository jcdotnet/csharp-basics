using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        //public Point(int x, int y) => (X, Y) = (x, y);

        public void GetCoords(out int x, out int y)
        {
            x = X; y = Y;
        }

        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);

        public static string DisplayPosition(Point point) => point switch
        {
            (0, 0) => "Origin",
            var (x, y) when x > 0 && y > 0 => $"Current Position is {x},{y}",
            _ => "Unknow Position"
        };
    }
}
