using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using System.Text.Json;

namespace Repositories
{
    // https://finnhub.io/
    // https://finnhub.io/docs/api/company-profile
    // https://finnhub.io/docs/api/quote
    public class FinnhubRepository : IFinnhubRepository
    {
        // dependencies
        private readonly IConfiguration _configuration; // in order to get the finnhub token
        // the IHttpClientFactory will create a HttpClient object for us (factory pattern) 
        private readonly IHttpClientFactory _httpClientFactory;
       
        // Dependency Injection (constructor injection)
        public FinnhubRepository(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;      
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                // Response.Content = response body
                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

                // converts from JSON to object
                // we got string attributes in the response body (address, alias, city, etc)
                // so the convert the JSON to a dictionary with string keys
                var dictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody) ?? throw new InvalidOperationException("No response from server");
                if (dictionary.ContainsKey("error"))
                    throw new InvalidOperationException(Convert.ToString(dictionary["error"]));

                return dictionary;
            }
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                    // Headers
                };
                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);

                // Response.Content = response body
                Stream stream = await httpResponse.Content.ReadAsStreamAsync();

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

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                    // Headers
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

                var responseDictionary = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(responseBody);

                if (responseDictionary == null)
                    throw new InvalidOperationException("No response from server");

                return responseDictionary;
            }
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                    // Headers
                };
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();

                var responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

                if (responseDictionary == null)
                    throw new InvalidOperationException("No response from server");

                if (responseDictionary.ContainsKey("error"))
                    throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

                return responseDictionary;
            }
        }
    }
}