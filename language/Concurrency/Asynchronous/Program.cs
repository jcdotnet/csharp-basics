// allows tasks to be executed concurrently without blocking the main thread 
// (alternative to continuation tasks)
// main use case: non-blocking IO operations (files, DB, network requests) 
// https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/

using Asyncrhonous;

Console.WriteLine("Asynchronous programming (TAP)");

FileIO.Demo();
Console.WriteLine();

var httpServerAsync = new HttpServerAsync("https://example.com");
// waits for the URL content (non-blocking: the thread is available for other tasks)
string urlContent = await httpServerAsync.GetUrlContent();
Console.WriteLine(urlContent); // urlContent available here