using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputOutput
{
    internal class FileInfoClass
    {
        public static void Demo()
        {
            // operations on a single file
            // https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo?view=net-9.0
            Console.WriteLine("\nFileInfo");

            var filePath        = "hi.txt";
            var destinationPath = "hi copy.txt";
            var movePath        = @"C:\programmingtemp\hi.txt";
            var fileInfo = new FileInfo(filePath); // FileInfo
            var moveInfo = new FileInfo(movePath);

            // Properties
            if (moveInfo.Exists)
            {
                Console.WriteLine(moveInfo.Exists);
                Console.WriteLine(moveInfo.Name);
                Console.WriteLine(moveInfo.Extension);
                Console.WriteLine(moveInfo.Length);
                Console.WriteLine(moveInfo.CreationTime);
                Console.WriteLine(moveInfo.LastAccessTime);
                Console.WriteLine(moveInfo.FullName);
                Console.WriteLine(moveInfo.DirectoryName);
            }
            // IO operations
            fileInfo.Create().Close();

            if (File.Exists(destinationPath)) {
                var destinationInfo = new FileInfo(destinationPath);
                destinationInfo.Delete();
            }

            var copiedFile = fileInfo.CopyTo(destinationPath);
            Console.WriteLine(copiedFile.Name + " has been created");

            if (File.Exists(movePath))
            {
                moveInfo.Delete();
            }
            fileInfo.MoveTo(movePath);
            Console.WriteLine($"{moveInfo.Name} has been created in {moveInfo.Directory}");
        }
    }
}
