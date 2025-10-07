using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    public class LongRunningTasksSimulation
    {
        private Thread? _thread;

        public void StartSimulation()
        {
            _thread = new Thread(RunSimulation);
            _thread.Priority = ThreadPriority.Highest;
            _thread.Start();
            // _thread.Join(); // let's keep the main thread running for the remaining examples 
        }

        private void RunSimulation()
        {
            for(int i = 0; i < 10000; i++)
            {
                // Perform complex calculations
            }
            Console.WriteLine("Long running tasks: simulation complete");
        }
    }
}
