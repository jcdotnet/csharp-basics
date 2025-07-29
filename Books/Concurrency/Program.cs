// See https://aka.ms/new-console-template for more information
// Examples from High Performance .NET, by Armen Melkumyan
using Concurrency;

// Thread class
var thread = new LongRunningTasksSimulation();
thread.StartSimulation();

Console.WriteLine("Main Thread - Threads");

// ThreadPool (used for short, repeated tasks)
var httpServer = new HttpServer();
httpServer.HandleRequest();

Console.WriteLine("Main Thread - ThreadPool");

// Background Workers (legacy, built on ThreadPool)
// Best suited for background tasks in WinForms or WPF applications
// For most scenarios, Task and async/await offers better performance and control
// Especially for I/O bound or more complex tasks.
// This is what I used the most for background tasks until 2016
var _ = new FileDownloader();

Console.WriteLine("Main Thread - Background Worker");

// Task Parallel Programming (TPL)
var primes = new ParallelPrimeNumberGenerator();
primes.Generate(100);

Console.WriteLine("Main Thread - TPL");

// Asynchronous Programming (async/await model)
var httpServerAsync = new HttpServerAsync("https://example.com");
// waits for the URL content (non-blocking: the thread is available for other tasks)
string urlContent = await httpServerAsync.GetUrlContent();
Console.WriteLine(urlContent); // here we have the URL content

Console.WriteLine("Main Thread - async/await");  

Console.ReadLine();