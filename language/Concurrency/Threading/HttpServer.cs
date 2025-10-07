using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Threading
{
    public class HttpServer
    {
        public void HandleRequest() // in real world scenario we would pass the request object
        {
            ThreadPool.QueueUserWorkItem(ProcessRequest);
            // ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessRequest), request);
        }

        private void ProcessRequest (object state)
        {
            Console.WriteLine($"Processing request on thread {Thread.CurrentThread.ManagedThreadId}");
            // simulates I/O bound work
            // HttpRequest request = state as HttpRequest;
            Thread.Sleep(100);
        }
    }
}
