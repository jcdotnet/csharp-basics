using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurrency
{
    public class LongRunningTasksSimulation
    {
        private Thread _thread;

        public void StartSimulation()
        {
            _thread = new Thread(RunSimulation);
            _thread.Priority = ThreadPriority.Highest;
            _thread.Start();
        }

        private void RunSimulation()
        {
            for(int i = 0; i < 10000; i++)
            {
                // Perform complex calculations
            }
            Console.WriteLine("Simulation Complete");
        }
    }
}
