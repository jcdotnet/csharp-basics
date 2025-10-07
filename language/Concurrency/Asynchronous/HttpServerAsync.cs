using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asyncrhonous
{
    public class HttpServerAsync
    {
        private readonly string _url;
        public HttpServerAsync(string url)
        {
            _url = url;
        }
        public async Task<string> GetUrlContent()
        {
            using var client = new HttpClient();
            Task<string> getStringTask =
                client.GetStringAsync(_url);

            DoIndependentWork();
            return await getStringTask;                    
        }

        void DoIndependentWork()
        {
            Console.WriteLine("Some independent work...");
        }
    }
}
