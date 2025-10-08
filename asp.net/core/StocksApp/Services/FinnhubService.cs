using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Text.Json;

namespace Services
{
    // https://finnhub.io/
    // https://finnhub.io/docs/api/company-profile
    // https://finnhub.io/docs/api/quote
    public class FinnhubService : IFinnhubService
    {
        // dependencies
        private readonly IConfiguration _configuration; // in order to get the finnhub token
        // the IHttpClientFactory will create a HttpClient object for us (factory pattern) 
        private readonly IHttpClientFactory _httpClientFactory;

        // Dependency Injection (constructor injection)
        public FinnhubService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public Dictionary<string, object>? GetCompanyProfile(string stockSymbol)
        {

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}") 
                };
                HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

                // Response.Content = response body
                string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

                // converts from JSON to object
                // we got string attributes in the response body (address, alias, city, etc)
                // so the convert the JSON to a dictionary with string keys
                var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody) ?? throw new InvalidOperationException("No response from server");
                if (dictionary.ContainsKey("error"))
                    throw new InvalidOperationException(Convert.ToString(dictionary["error"]));

                return dictionary;
            }

        }

        public Dictionary<string, object>? GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                    // Headers
                };
                HttpResponseMessage httpResponse = httpClient.Send(httpRequest);

                // Response.Content = response body
                Stream stream = httpResponse.Content.ReadAsStream();
                
                //string response;
                //using (StreamReader reader = new StreamReader(stream))
                //{
                //    response = reader.ReadToEnd();
                //}
                using StreamReader reader = new(stream);
                string response = reader.ReadToEnd();

                // converts from JSON to object
                // we got string attributes in the response body (c: current price, etc),
                // so the convert the JSON to a dictionary with string keys
                var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (dictionary == null)
                    throw new InvalidOperationException("No response from server");

                if (dictionary.ContainsKey("error"))
                    throw new InvalidOperationException(Convert.ToString(dictionary["error"]));

                return dictionary;
            }
        }
    }
}
