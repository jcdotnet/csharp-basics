// System.IO
// https://learn.microsoft.com/en-us/dotnet/api/system.io?view=net-9.0

using InputOutput;

FileClass.Demo();
FileInfoClass.Demo();
// https://learn.microsoft.com/en-us/dotnet/api/system.io.directory?view=net-9.0
// https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo?view=net-9.0
// https://learn.microsoft.com/en-us/dotnet/api/system.io.driveinfo?view=net-9.0
FileStreamClass.Demo();
StreamWriterReader.Demo();
// https://learn.microsoft.com/en-us/dotnet/api/system.io.binarywriter?view=net-9.0
// https://learn.microsoft.com/en-us/dotnet/api/system.io.binaryreader?view=net-9.0
string filePath = "example.txt";                            // bin folder
string textToWrite = "Hello, this is a test message!";

using (StreamWriter writer = new StreamWriter(filePath))    // ensures StreamWriter is properly disposed
{
    writer.WriteLine(textToWrite);
}

var numbers = new List<int>();
using (StreamReader reader = File.OpenText("numbers.txt"))  // existing file in the bin folder
{
    string line;
    while ((line = reader.ReadLine()) is not null)
    {
        if (int.TryParse(line, out int number))
        {
            numbers.Add(number);
        }
    }
}

foreach (var number in numbers)
{
    Console.WriteLine(number);
}

// bonus (getting from remote files): simple web scrapper
using (HttpClient httpClient = new HttpClient()) // using (var webClient = new WebClient()) // deprecated
{
    //string googleHome = webClient.DownloadString("https://google.com");
    string googleHome = await httpClient.GetStringAsync("https://example.com");
    Console.WriteLine(googleHome);
}

/*
 * Not using top level statements
namespace InputOutput
{
    internal class Program
    {
        static void Main()
        {
            FileClass.Demo();
            // ...
        }
    }
}
*/