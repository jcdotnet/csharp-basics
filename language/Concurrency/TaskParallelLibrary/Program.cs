// TPL (= set of API and runtimes features based on threading, which provide high-level 
// abstraction to implement concurrency, parallelism and asynchronous programming
// Task (= operation or method that can be executed concurrently & parallel with other tasks)
// use cases:
// multithreaded computations where CPU-bound tasks can be parallelized
// asyncrhonous I/O operations
// parallel processing of collections using Parallel.ForEach or PLINQ
// responsive UI that require non-blocking operations (background tasks, etc)
// https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/
// https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task?view=net-9.0
using TaskParallelLibrary;

Console.WriteLine("Task Parallel Library (TPL)");

//TaskClass.TaskRunDemo();
//TaskClass.TaskStartNewDemo();
TaskClass.TaskStopWatchDemo();
//TaskClass.TaskTResultDemo();
//TaskClass.TaskWaitAnyDemo();
TaskClass.TasksChainDemo();

FileIO.Demo();

var primes = new ParallelPrimeNumberGenerator();
primes.Generate(100);