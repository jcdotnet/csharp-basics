using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskParallelLibrary
{
    class FileWriter
    {
        public Task Write(string fileName, string content)
        {
            Task writerTask;
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                writerTask = streamWriter.WriteAsync(content);
            }
            return writerTask;
        }             
    }

    class FileReader
    {
        public Task<string> Read(string fileName)
        {
            //Task<string> readerTask;
            StreamReader streamReader = new StreamReader(fileName);
            //using (StreamReader streamReader = new StreamReader(fileName))
            //{
                Task<string> readerTask = streamReader.ReadToEndAsync();
            //}
            return readerTask;
        }
    }

    internal class FileIO
    {
        public static void Demo()
        {
            //File.Create("demo.txt").Close();
            string fileName = "demo.txt";
            string content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            FileWriter fileWriter = new FileWriter();
            FileReader fileReader = new FileReader();

            Task writerTask = fileWriter.Write(fileName, content);
            
            // blocks the main thread: solution async/await will be covered later
            // we can use continuation task instead as workaround in the meantime
            writerTask.Wait(); // blocks until writerTask completion

            Task<string> readerTask = fileReader.Read(fileName);
            readerTask.Wait();

            Console.WriteLine("File content:");
            Console.WriteLine(readerTask.Result);
        }
    }
}
