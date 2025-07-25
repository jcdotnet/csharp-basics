using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Features
{
    public interface ICar
    {
        public void Start() => Console.WriteLine("Car has started"); // C#8: default implementation of interface methods
        public void Run();

        public void Run(double speed) => Console.WriteLine($"Car is running at {speed}"); // C#8: default implementation of interface methods
    }
}
