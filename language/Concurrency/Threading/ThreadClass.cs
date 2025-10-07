using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    internal class ThreadClass
    {
        private static Counter counter = new Counter();

        // still in the main thread
        public static void SigleThreadedAppDemo()
        {
            Thread mainThread = Thread.CurrentThread;   // accesing the current thread
            mainThread.Name = "Main Thread";            // giving the current thread a name
            //Console.WriteLine(mainThread.Priority);     // Priority: normal (default)
            //Console.WriteLine(mainThread.IsAlive);      // IsAlive: True
            //Console.WriteLine(mainThread.IsBackground); // IsBackground: False
            //Console.WriteLine(mainThread.ThreadState);  // ThreadState: Running    
            counter.GreenIncCounter();
            counter.RedDecCounter();
        }

        public static void MultipleThreadsDemo()
        {
            MultipleThreads(false, false);

        }

        public static void MultipleThreadsWithCallBackDemo()
        {
            Action<long> callback = sum => { Console.WriteLine($"Green counter sum: {sum}"); };
            ThreadStart threadStart1 = new ThreadStart(() => { counter.GreenIncCounter(callback); });
            Thread thread1 = new Thread(threadStart1);
            thread1.Start();

            ThreadStart threadStart2 = new ThreadStart(counter.RedDecCounter);
            Thread thread2 = new Thread(threadStart2);
            thread2.Start();

            thread1.Join();
            thread2.Join();
            Console.ResetColor();
        }

        public static void MultipleThreadsWithInterruptionDemo()
        {
            MultipleThreads(false, true);
        }

        public static void MultipleThreadsWithPriorityDemo()
        {
            MultipleThreads(true, false);
        }

        private static void MultipleThreads(bool setPriority, bool willInterrupt)
        {
            Thread mainThread = Thread.CurrentThread;
            mainThread.Name = "Main Thread";

            ThreadStart threadStart1 = new ThreadStart(counter.GreenIncCounter);

            // passing a method with parameters e.g. Counter(int count) by using a lambda expression
            // without parameters so it matches the ThreadStart delegate: public void ThreadStart()
            //ThreadStart threadStart1 = new ThreadStart(() => { counter.GreenIncCounter(20); });

            // or we can use the ParameterizedThreadStart delegate
            //ParameterizedThreadStart threadStart1 = new ParameterizedThreadStart(counter.GreenIncCounter);

            Thread thread1 = new Thread(threadStart1) { Name = "Thread 1" };
            if (setPriority)
            {
                // priority will make no differece here because of the sleep call
                // in real world apps, we may want to allocate more CPU time, etc
                thread1.Priority = ThreadPriority.Highest;
            }
            Console.WriteLine($"{thread1.Name} ({thread1.ManagedThreadId}) state is {thread1.ThreadState}");
            thread1.Start();
            // in case we are using a ParameterizedThreadStart delegate,
            // we can pass the method arguments to the Start method
            //thread1.Start(20);
            Console.WriteLine($"{thread1.Name} ({thread1.ManagedThreadId}) state is {thread1.ThreadState}");

            ThreadStart threadStart2 = new ThreadStart(counter.RedDecCounter);
            Thread thread2 = new Thread(threadStart2) { Name = "Thread 2" };
            if (setPriority)
            {
                thread1.Priority = ThreadPriority.BelowNormal;
            }

            Console.WriteLine($"{thread2.Name} ({thread2.ManagedThreadId}) state is {thread2.ThreadState}");
            thread2.Start();
            Console.WriteLine($"{thread2.Name} ({thread2.ManagedThreadId}) state is {thread2.ThreadState}");

            if (willInterrupt)
            {
                // interrupting thread 1 after half a second
                Thread.Sleep(1000);
                thread1.Interrupt();
                // blocking the main thread until completion of thread2  
                thread2.Join();
            }
            else
            {
                // blocking main thread until completion of thread1 and thread2
                thread1.Join();
                thread2.Join();
            }
            Console.ResetColor();
        }

        public static void LongRunningTasksDemo()
        {
            var thread = new LongRunningTasksSimulation();
            thread.StartSimulation();
        }
        public static void SharedResourceDemo()
        {
            // Increment Counter also increments the ShareResource value
            // Decrement Counter also decrements the ShareResource value
            // therefore, the final value for SharedResource should be 0
            ThreadStart threadStart1 = new ThreadStart(counter.IncrementCounter);
            Thread thread1 = new Thread(threadStart1);
            thread1.Start();

            ThreadStart threadStart2 = new ThreadStart(counter.DecrementCounter);
            Thread thread2 = new Thread(threadStart2);
            thread2.Start();

            thread1.Join();
            thread2.Join();
            // the resource value is different than 0 !!!
            Console.WriteLine("Shared resource value: " + Shared.SharedResource);
            // unexpected result because of the race condition!!! (= two or more
            // threads access and try to change the shared data at the same time)
            //
            // deadlock is another problem with shared resources, it occurs when
            // threads 1 and 2 are waiting at the same time for the resource, so
            // the program becomes unresponsive (no thread can proceed = standstill)
            // 
            // starvation happens when a thread cannot progress because a thread
            // with higher priority consistently acquires the shared resource first
        }

        public static void SharedResourceMonitorDemo()
        {
            // Increment Counter also increments the ShareResource value
            // Decrement Counter also decrements the ShareResource value
            // therefore, the final value for SharedResource should be 0
            ThreadStart threadStart1 = new ThreadStart(counter.IncrementCounter2);
            Thread thread1 = new Thread(threadStart1);
            thread1.Start();

            ThreadStart threadStart2 = new ThreadStart(counter.DecrementCounter2);
            Thread thread2 = new Thread(threadStart2);
            thread2.Start();

            thread1.Join();
            thread2.Join();
            // the resource value is 0 !!!
            Console.WriteLine("Shared resource value: " + Shared.SharedResource);
        }
    }
}
