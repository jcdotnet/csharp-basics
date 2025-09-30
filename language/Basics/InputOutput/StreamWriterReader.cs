using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputOutput
{
    internal class StreamWriterReader
    {
        public static void Demo()
        {
            // writing content to / reading content from files (by using strings)
            // https://learn.microsoft.com/es-es/dotnet/api/system.io.streamwriter?view=net-8.0
            // https://learn.microsoft.com/es-es/dotnet/api/system.io.streamreader?view=net-8.0
            Console.WriteLine("\nStreamWriter / StreamReader");
            var filePath = "demo.txt";
            // creation (1)
            // StreamWriter writer1 = new StreamWriter(filePath);
            
            // creation (2)
            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            StreamWriter writer2 = new StreamWriter(fileStream);
            writer2.WriteLine("Hello, StreamWriter class!");
            writer2.Close();

            // creation (3) preferred way
            // using (var writer = new StreamWriter(filePath))
            FileInfo fileInfo = new FileInfo(filePath);
            using (var writer = fileInfo.CreateText())
            {
                writer.WriteLine("Hello, StreamWriter class!");
                writer.WriteLine("Now we don't have to use byte[]");
            } // Close() is called here

            // StreamReader
            // FileStream fileStream2 = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            // using (var reader = new StreamReader(fileStream2))
            // using (var reader = new StreamReader(filePath)
            using (var reader = fileInfo.OpenText())
            {
                Console.WriteLine(reader.ReadToEnd());
            } // Close() is called here
        }
    }
}
