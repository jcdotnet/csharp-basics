using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskParallelLibrary
{
    internal class TaskClass
    {
        private static readonly Counter counter = new();

        public static void TaskRunDemo()
        {
            CountdownEvent countdownEvent = new CountdownEvent(2); // 2 tasks
                                                                   
            // Task.Run is the most common and preferred way to create a task
            Task task1 = Task.Run(() => // Action delegate (no params, no return)
            {
                counter.GreenIncCounter(20);
                countdownEvent.Signal();
            });

            Task task2 = Task.Run(() =>
            {
                counter.RedDecCounter(20);
                countdownEvent.Signal();
            });

            countdownEvent.Wait(); // we wil use Task.Wait / Task.WaitAll later 
            Console.ResetColor();
        }

        public static void TaskStartNewDemo()
        {

            Task task1 = Task.Factory.StartNew(() => // Action: Task 
            {
                counter.GreenIncCounter(20);
            });

            Task task2 = Task.Factory.StartNew(() => // Action: Task
            {
                counter.RedDecCounter(20);
            });

            // task1.Wait(); // similar to join() in threading
            // task2.Wait();
            Task.WaitAll(); // waits for all tasks
            Console.ResetColor();
        }

        public static void TaskStopWatchDemo()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            TaskRunDemo();
            stopwatch.Stop();
            Console.WriteLine($"Tasks execution time was {stopwatch.ElapsedMilliseconds} ms");
        }
        public static void TaskTResultDemo()
        {
            Task<long> task1 = Task.Factory.StartNew(() => // Func<long> : Task<long>
            {
                return counter.IncSumCounter(20);
            });

            Task<long> task2 = Task.Factory.StartNew(() => // Func<long> : Task<long>
            {
                return counter.DecSumCounter(20);
            });
            Task.WaitAll(task1, task2);
            Console.WriteLine($"\nCounters sum value is {task1.Result + task2.Result}");
        }

        public static void TaskWaitAnyDemo()
        {
            Task<long>task1 = Task.Factory.StartNew(() => // Func<long> : Task<long>
            {
                return counter.IncSumCounter(20);
            });

            Task<long> task2 = Task.Factory.StartNew(() => // Func<long> : Task<long>
            {
                return counter.DecSumCounter(20);
            });
            int completedTaskIndex = Task.WaitAny(task1, task2); // params Task[] tasks
            if (completedTaskIndex  == 0) // task1
                Console.WriteLine($"\nCounter 1 sum value is {task1.Result}");
            else // task2
                Console.WriteLine($"\nCounter 2 sum value is {task2.Result}");
        }
        public static void TasksChainDemo()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            /*
             * Task.Run
            Task.Run(() =>
            {
                return counter.IncSumCounterExc(20);
            }).ContinueWith((antecedent) =>
            {
                if (antecedent.Status == TaskStatus.Faulted)
                {
                    Console.WriteLine(antecedent.Exception?.InnerExceptions.First().Message);
                    return -1;
                } else
                {              
                    return antecedent.Result;
                }
            }).ContinueWith((antecedent) =>
            {
                Console.WriteLine($"\nCounter 1 sum value is {antecedent.Result}");
            });
            */
            Task task1 = Task.Factory.StartNew(() =>
            {
                return counter.IncSumCounterExc(20, cts.Token);
            }, cts.Token).ContinueWith((antecedent) =>
            {
                if (antecedent.Status == TaskStatus.Canceled)
                {
                    Console.WriteLine("Increment Counter Canceled");
                    return -1;
                }
                if (antecedent.Status == TaskStatus.Faulted)
                {
                    Console.WriteLine(antecedent.Exception?.InnerExceptions.First().Message);
                    return -1;
                }
                else
                {
                    return antecedent.Result;
                }
            }).ContinueWith((antecedent) =>
            {
                Console.WriteLine($"\nCounter 1 sum value is {antecedent.Result}");
            }); ;

            Task<long> task2 = Task.Factory.StartNew(() =>
            {
                return counter.DecSumCounter(20);
            });

            Task continuationTask2 = task2.ContinueWith((antecedent) =>
            {
                Console.WriteLine($"\nCounter 2 sum value is {antecedent.Result}");
            });

            // cancel task1
            //Task.Delay(500).Wait();
            //cts.Cancel();

            Task.WaitAll(task1, task2); // blocking the current thread
        }
    }
}
