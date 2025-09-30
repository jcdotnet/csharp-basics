using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputOutput
{
    internal class FileClass
    {
        public static void Demo()
        {
            // https://learn.microsoft.com/en-us/dotnet/api/system.io.file?view=net-9.0
            var filePath = "hello.txt";
            File.Create(filePath).Close();

            if (!File.Exists(filePath))
            {
                Console.WriteLine(filePath + " does not found");
                return;
            }
            // var content = "Hello, File class examples";
            // File.WriteAllText(filePath, content); // will override the existing content

            var lines = new List<string>() { "Hello, File", "Fun with IO", "Don't forget to close your files" };
            File.WriteAllLines(filePath, lines);

            var destinationPath = "hello copy.txt";

            //var i = 0;
            //while (File.Exists(destinationPath))
            //{
            //    destinationPath = destinationPath.Insert(destinationPath.Length - 4, " copy");
            //    i++;
            //}
            //if (i < 4)
            //{
            //    File.Copy(filePath, destinationPath);
            //    Console.WriteLine(filePath + " copied to " + destinationPath);
            //}

            if (File.Exists(destinationPath))
                File.Delete(destinationPath);
            
            File.Copy(filePath, destinationPath);
            Console.WriteLine(filePath + " copied to " + destinationPath);

            // https://learn.microsoft.com/en-us/dotnet/api/system.io.directory?view=net-9.0
            Directory.CreateDirectory(@"C:\programmingtemp");

            var movePath = @"C:\programmingtemp\" + filePath;
            if (!File.Exists(movePath))
            {
                File.Move(filePath, movePath);
                Console.WriteLine(filePath + " moved to " + movePath);
                //Console.WriteLine("File content: ", File.ReadAllText(movePath));
                var fileLines = File.ReadAllLines(movePath);
                Console.WriteLine("File Content:");
                foreach (var line in fileLines) Console.WriteLine(line);
            }
        }
    }
}
