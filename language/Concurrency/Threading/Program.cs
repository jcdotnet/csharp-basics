// Threads and Threading (= a thread is an unit of execution within a process)
// https://learn.microsoft.com/en-us/dotnet/standard/threading/threads-and-threading
// https://learn.microsoft.com/en-us/dotnet/standard/threading/using-threads-and-threading
// https://learn.microsoft.com/en-us/dotnet/api/system.threading.thread?view=net-9.0
using Threading;

Console.WriteLine("Threading");

Console.WriteLine("Single thread");
ThreadClass.SigleThreadedAppDemo();

Console.WriteLine();

Console.WriteLine("Multi thread");
ThreadClass.MultipleThreadsDemo();

// below counters w/ white text because the main thread waited for running threads to complete 
Console.WriteLine("Current thread (Program.cs): " + Thread.CurrentThread.Name); // Main Thread 
Console.WriteLine();

//Console.WriteLine("Multi thread with priority");
//ThreadClass.MultipleThreadsWithPriorityDemo();
//Console.WriteLine();

Console.WriteLine("Multi thread with interruption");
ThreadClass.MultipleThreadsWithInterruptionDemo();
Console.WriteLine();

Console.WriteLine("Multi thread with callback");
ThreadClass.MultipleThreadsWithCallBackDemo();
Console.WriteLine();

Console.WriteLine("Long running tasks demo");
ThreadClass.LongRunningTasksDemo();
Console.WriteLine();

Console.WriteLine("Shared resources");
//ThreadClass.SharedResourceDemo();
ThreadClass.SharedResourceMonitorDemo();
//SharedResourcesSemaphoreDemo();
//SharedResourcesMutexDemo();

//Interaction1.ThreadInteractionDemo();
//Interaction2.ThreadInteractionDemo();
//Interaction3.ThreadInteractionDemo();
//Interaction4.ThreadInteractionDemo();
//Interaction5.ThreadInteractionDemo();
//BankOperations.BankOperationsDemo(); //
Console.WriteLine();

// Thread Pool (used for short, repeated tasks)
Console.WriteLine("Thread Pool"); 

var httpServer = new HttpServer();
httpServer.HandleRequest();
Console.WriteLine();

Console.WriteLine("Bacground Workers");
// Background Workers (legacy, built on ThreadPool)
// Best suited for background tasks in WinForms or WPF applications
// For most scenarios, Task and async/await offers better performance and control
// Especially for I/O bound or more complex tasks.
// This is what I used the most for background tasks until 2016
var _ = new FileDownloader();