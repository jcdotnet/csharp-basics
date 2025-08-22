// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Http;

string filePath = "example.txt"; // bin folder
string textToWrite = "Hello, this is a test message!";

using (StreamWriter writer = new StreamWriter(filePath)) // ensures the StreamWriter is properly disposed
{
    writer.WriteLine(textToWrite);
}

var numbers = new List<int>();
using (StreamReader reader = File.OpenText("numbers.txt"))
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


// simple web scrapper
// using (WebClient webClient = new WebClient()) // WebClient is obsolete
using (HttpClient httpClient = new HttpClient() )
{
    //string googleHome = webClient.DownloadString("https://google.com");
    string googleHome = await httpClient.GetStringAsync("https://example.com");
    Console.WriteLine(googleHome);

}