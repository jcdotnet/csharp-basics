using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public interface ICar
    {
        // C#8: default implementation of interface methods
        public void Start() => Console.WriteLine("Car has started");
        public void Run();

        // C#8: default implementation of interface methods
        public void Run(double speed) => Console.WriteLine($"Car is running at {speed}");
    }
}
