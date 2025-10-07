using Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    class Shared5
    {
        // public static object LockObject = new object();
        public static ConcurrentQueue<int> Buffer = new ConcurrentQueue<int>();
        public const int BufferCapacity = 5;
        public static ManualResetEvent ProducerEvent = new ManualResetEvent(true);
        public static ManualResetEvent ConsumerEvent = new ManualResetEvent(false);

        public static void Print()
        {
            Console.Write("Buffer: ");
            foreach (int i in Buffer)
            {
                Console.Write($"{i} ");

            }
            Console.WriteLine();
        }
    }

    class Producer5 // producer thread
    {
        public void Produce()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} thread started");
            for (int i = 0; i < 10; i++)
            {
                if (Shared5.Buffer.Count == Shared5.BufferCapacity)
                {
                    Console.WriteLine("Buffer is full, waiting for signal from the consumer");
                    Shared5.ProducerEvent.Reset();
                }

                Shared5.ProducerEvent.WaitOne();

                Shared5.Buffer.Enqueue(i + 1);

                Console.WriteLine("Producer: " + i + 1);
                Shared5.Print();


                Shared5.ConsumerEvent.Set();
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} thread completed");
        }
    }
    class Consumer5 // consumer thread
    {
        public void Consume()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} thread started");

            for (int i = 0; i < 10; i++)
            {

                if (Shared5.Buffer.Count == 0)
                {
                    Shared5.ConsumerEvent.Reset();
                }

                Shared5.ConsumerEvent.WaitOne();


                Console.WriteLine("Processing data...");
                Thread.Sleep(1000);

                bool isSuccess = Shared5.Buffer.TryDequeue(out int value);
                if (isSuccess)
                {
                    Console.WriteLine("Consumer: " + value);
                }

                Shared5.ProducerEvent.Set();
            }
 
            Console.WriteLine($"{Thread.CurrentThread.Name} thread completed");
        }
    }

    internal class Interaction5
    {
        public static void ThreadInteractionDemo()
        {
            // Concurrent collections (thread-safe implementations of collections)
            // with ManualResetEvent

            Producer5 producer = new Producer5();
            Consumer5 consumer = new Consumer5();

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
