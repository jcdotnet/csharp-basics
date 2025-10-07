using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    class Shared4
    {
        public const int BufferCapacity = 5;

        public static object LockObject = new object();
        public static Queue<int> Buffer = new Queue<int>();

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

    class Producer4 // producer thread
    {
        public void Produce()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} thread started");
            
            for (int i = 0; i < 10; i++)
            {
                if (Shared4.Buffer.Count == Shared4.BufferCapacity)
                {
                    Console.WriteLine("Buffer is full, waiting for signal from the consumer");
                    lock (Shared4.LockObject)
                    {
                        Monitor.Wait(Shared4.LockObject);
                    }
                }
                Shared4.Buffer.Enqueue(i + 1);
                
                Console.WriteLine("Producer: " + i+1);
                Shared4.Print();

                lock (Shared4.LockObject)
                {
                    Monitor.Pulse(Shared4.LockObject); // motifies (wakes up) the consumer thread
                }
                
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} thread completed");
        }
    }

    class Consumer4 // consumer thread
    {
        public void Consume()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} thread started");
            // waits (blocks the current thread) until the event receives a signal
            Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for the producer thread");

            for (int i = 0; i < Shared4.BufferCapacity; i++)
            {
                lock (Shared4.LockObject)
                { // Monitor.Enter
                    if (Shared4.Buffer.Count == 0)
                    {
                        Monitor.Wait(Shared4.LockObject);
                    }
                } // Monitor.Exit

                Console.WriteLine("Processing data...");
                Thread.Sleep(1000);

                lock (Shared4.LockObject)
                {
                    int value = Shared4.Buffer.Dequeue();
                    Console.WriteLine("Consumer: " + value);

                    Monitor.Pulse(Shared4.LockObject); // motifies the producer
                }
            }           
            Console.WriteLine($"{Thread.CurrentThread.Name} thread completed");
        }
    }

    internal class Interaction4
    {
        public static void ThreadInteractionDemo()
        {
            // Thread interaction, or signaling
            Producer4 producer = new Producer4();
            Consumer4 consumer = new Consumer4();

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
