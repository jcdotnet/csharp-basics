using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    class Shared1
    {
        public static int[] Data { get; set; }
        public static int DataCount { get; set; }
        public static ManualResetEvent ManualResetEvent {  get; set; }

        static Shared1()
        {
            Data = new int[12];
            DataCount = Data.Length;
            ManualResetEvent = new ManualResetEvent(false); // unsignaled
        }
    }

    class Producer // producer thread
    {
        public void Produce()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} thread started");
            for (int i = 0; i < Shared1.Data.Length; i++)
            {
                Shared1.Data[i] = i + 1;
                Thread.Sleep(100);
            }
            Shared1.ManualResetEvent.Set(); // setting the signal after finishing
            Console.WriteLine($"{Thread.CurrentThread.Name} thread completed");
        }
    }

    class Consumer // consumer thread
    {
        public void Consume()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} thread started");
            // waits (blocks the current thread) until the event receives a signal
            Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for the producer thread");
            Shared1.ManualResetEvent.WaitOne();
            Console.WriteLine("Printing data from Consumer:");
            for (int i = 0; i < Shared1.Data.Length; i++)
            {
                Console.WriteLine(Shared1.Data[i]);
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} thread completed");

        }
    }
    internal class Interaction1
    {
        public static void ThreadInteractionDemo()
        {
            // Thread interaction, or signaling
            Producer producer = new Producer();
            Consumer consumer = new Consumer();

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
