using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    class Shared3
    {
        public static int[] Data { get; set; }
        public static int BatchCount { get; set; }
        public static int BatchSize { get; set; }
        public static AutoResetEvent AutoResetEvent { get; set; }

        static Shared3()
        {
            Data = new int[12];
            BatchCount = 4;
            BatchSize = 3;
            AutoResetEvent = new AutoResetEvent(false); // unsignaled
        }
    }

    class Producer3 // producer thread
    {
        public void Produce()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} thread started");
            for (int i = 0; i < Shared3.BatchCount; i++)
            {
                for (int j = 0; j < Shared3.BatchSize; j++)
                {
                    Shared3.Data[i * Shared2.BatchSize + j] = (i * Shared2.BatchSize) + j + 1;
                    Thread.Sleep(100);
                }

                // Manual
                //Shared2.ManualResetEvent.Set(); // setting the signal after finishing 
                //Shared2.ManualResetEvent.Reset(); // unsignaled (makes the consumer to wait)
                Shared3.AutoResetEvent.Set();
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} thread completed");
        }
    }

    class Consumer3 // consumer thread
    {
        public void Consume()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} thread started");
            // waits (blocks the current thread) until the event receives a signal
            Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for the producer thread");

            for (int i = 0; i < Shared2.BatchCount; i++)
            {
                Shared3.AutoResetEvent.WaitOne();
                Console.WriteLine("Printing data from Consumer:");
                for (int j = 0; j < Shared3.BatchSize; j++)
                {
                    Console.WriteLine(Shared3.Data[i * Shared3.BatchSize + j]);
                }
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} thread completed");
        }
    }

    internal class Interaction3
    {
        public static void ThreadInteractionDemo()
        {
            // Thread interaction, or signaling
            Producer3 producer = new Producer3();
            Consumer3 consumer = new Consumer3();

            ThreadStart threadStart1 = new ThreadStart(producer.Produce);
            ThreadStart threadStart2 = new ThreadStart(consumer.Consume);

            Thread producerThread = new Thread(threadStart1) { Name = "Producer" };
            Thread consumerThread = new Thread(threadStart2) { Name = "Consumer" };

            producerThread.Start();
            consumerThread.Start();

            producerThread.Join();
            consumerThread.Join();
        }
    }
}
