namespace Threading
{
    public class Shared
    {
        public static int SharedResource { get; set; }
        public static readonly object lockObject = new(); // new object()
    }
    public class Counter
    {
        // we can set the count value here rather than passing it in to the method
        // public int Count { get; set; } 

        public void GreenIncCounter()
        {   
            GreenIncCounter(20);
        }
        public void GreenIncCounter(Action<long>? callback = null)
        {
            GreenIncCounter(20, callback);
        }
        public void GreenIncCounter(object? count, Action<long>? callback = null)
        {
            long sum = 0;
            try
            {
                for (int i = 0; i < (int?)count; i++)
                {
                    sum += i;
                    Console.ForegroundColor = ConsoleColor.Green;
                    // pauses the thread for 100 ms, the thread state changes to WaitSleepJoin
                    // while in sleep period, the thread is not executing code / consuming CPU
                    Thread.Sleep(100);
                    Console.Write($"{i + 1} ");
                }
                
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine(Thread.CurrentThread.Name + " interrumped");
            }
            finally
            {
                callback?.Invoke(sum); //  if (callback != null) callback(sum);
            }
            Console.WriteLine("Current Thread (Green Counter): " + Thread.CurrentThread.Name);
        }

        public void IncrementCounter()
        {
            for (int i = 0; i < 20; i++)
            {
                Shared.SharedResource++; // critical code
                Thread.Sleep(100);
                Console.Write($"{i + 1} ");
            }
        }
        public void IncrementCounter2()
        {
            for (int i = 0; i < 20; i++)
            {
                //Monitor.Enter(Shared.lockObject); // adquires an exclusive lock on the passed object
                //Shared.SharedResource++; // critical code
                //Monitor.Exit(Shared.lockObject); // releases the lock
                
                lock (Shared.lockObject) // same with the lock syntax
                {
                    Shared.SharedResource++;
                }
                Thread.Sleep(100);
                Console.Write($"{i + 1} ");
            }
        }

        public void RedDecCounter()
        {
            RedDecCounter(20);
        }
        public void RedDecCounter(object? count)
        {
            for (int? i = (int?)count; i > 0; i--)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // pauses the tHread for 1000 ms, the thread state changes to WaitSleepJoin
                // while in sleep period, the thread is not executing code / consuming CPU 
                Thread.Sleep(100);
                Console.Write($"{i} ");
            }
            Console.WriteLine("Current Thread (Red Counter): " + Thread.CurrentThread.Name);
        }

        public void DecrementCounter()
        {
            for (int i = 20; i > 0; i--)
            {
                Shared.SharedResource--;
                Thread.Sleep(100);
                Console.Write($"{i} ");
            }
        }
        public void DecrementCounter2()
        {
            for (int i = 20; i > 0; i--)
            {
                //Monitor.Enter(Shared.lockObject);
                //Shared.SharedResource--;
                //Monitor.Exit(Shared.lockObject);
                
                lock (Shared.lockObject) // same with the lock syntax
                {
                    Shared.SharedResource--;
                }
                Thread.Sleep(100);
                Console.Write($"{i} ");
            }
        }
    }
}
