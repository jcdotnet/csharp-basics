using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    class Shared2
    {
        public static int[] Data { get; set; }
        public static int BatchCount { get; set; }
        public static int BatchSize { get; set; }
        public static ManualResetEvent ManualResetEvent { get; set; }

        static Shared2()
        {
            Data = new int[12];
            BatchCount = 4;
            BatchSize = 3;
            ManualResetEvent = new ManualResetEvent(false); // unsignaled
        }
    }

    class Producer2 // producer thread
    {
        public void Produce()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} thread started");
            for (int i = 0; i < Shared2.BatchCount; i++)
            {
                for (int j = 0; j < Shared2.BatchSize; j++)
                {
                    Shared2.Data[i * Shared2.BatchSize + j] = (i * Shared2.BatchSize) + j + 1;
                    Thread.Sleep(100);
                }
                Shared2.ManualResetEvent.Set(); // setting the signal after finishing
                
                Shared2.ManualResetEvent.Reset(); // unsignaled (makes the consumer to wait)
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} thread completed");
        }
    }

    class Consumer2 // consumer thread
    {
        public void Consume()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} thread started");
            // waits (blocks the current thread) until the event receives a signal
            Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for the producer thread");

            for (int i = 0; i < Shared2.BatchCount; i++)
            {
                Shared2.ManualResetEvent.WaitOne();
                Console.WriteLine("Printing data from Consumer:");
                for (int j = 0; j < Shared2.BatchSize; j++)
                {
                    Console.WriteLine(Shared2.Data[i * Shared2.BatchSize + j]);
                }
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} thread completed");
        }
    }

    internal class Interaction2
    {
        public static void ThreadInteractionDemo()
        {
            // Thread interaction, or signaling
            Producer2 producer = new Producer2();
            Consumer2 consumer = new Consumer2();

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
