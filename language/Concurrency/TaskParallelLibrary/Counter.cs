using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskParallelLibrary
{
    internal class Counter
    {
        public void GreenIncCounter(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{i + 1} ");
            }
            Console.WriteLine("Current Thread: " + Thread.CurrentThread.Name); // .NET TP Worker
        }

        public long IncSumCounter(int count)
        {
            long sum = 0;
            for (int i = 0; i < count; i++)
            {
                Task.Delay(100).Wait(); // use with caution, esp in real world apps
                Console.Write($"{i + 1} ");
                sum += i;
            }
            return sum;
        }

        public long IncSumCounterExc(int count, CancellationToken token)
        {
            long sum = 0;
            for (int i = 0; i < count; i++)
            {
                // if (token.IsCancellationRequested) continue;
                token.ThrowIfCancellationRequested(); // throw new TaskCancelledException
                Task.Delay(100).Wait();
                Console.Write($"{i + 1} ");
                sum += i;
            }
            var random = new Random();
            if (random.Next(2) == 1) throw new Exception("An exception ocurred");
            return sum;
        }

        public void RedDecCounter(int count)
        {
            for (int i = count; i > 0; i--)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{i} ");
            }
            Console.WriteLine("Current Thread: " + Thread.CurrentThread.Name); // .NET TP Worker
        }

        public long DecSumCounter(int count)
        {
            long sum = 0;
            for (int i = count; i > 0; i--)
            {
                Task.Delay(100).Wait(); // use with caution, esp in real world apps
                Console.Write($"{i} ");
                sum += i;  
            }
            return sum;
        }
    }
}
