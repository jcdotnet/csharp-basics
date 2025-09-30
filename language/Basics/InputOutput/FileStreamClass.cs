using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputOutput
{
    internal class FileStreamClass
    {
        public static void Demo()
        {
            // writing content to / reading content from files (by using streams / byte[])
            // https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-9.0
            Console.WriteLine("\nFileStream");

            var filePath = "test.txt";
            // FileStream  fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            // var fileStream = File.Create(filePath); // same // FileMode.Create / FileAccess.Write 
            // var fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write); // also same
            var fileStream = File.OpenWrite(filePath); // same but FileMode is OpenOrCreate

            // FileInfoClass also has methods that returns FileStream
            // FileInfo fileInfo = new FileInfo(filePath);
            // var fileStream = fileInfo.Create();
            // var fileStream = fileInfo.Open(FileMode.Create);
            // var fileStream = fileInfo.OpenWrite();
            string content = "Hello, FileStream class!";
            byte[] bytes = Encoding.ASCII.GetBytes(content);

            // fileStream.Write(bytes);
            fileStream.Write(bytes, 0, bytes.Length);
            // performing multiple operations on the file...
            fileStream.Close();

            Console.WriteLine(filePath + " has been created");

            var fileStream2 = File.OpenRead(filePath);
            var fileBytes   = new byte[fileStream2.Length];
            fileStream2.Read(fileBytes, 0, fileBytes.Length);
            fileStream2.Close();

            Console.WriteLine("File Content:\n" + Encoding.ASCII.GetString(fileBytes));


        }
    }
}
