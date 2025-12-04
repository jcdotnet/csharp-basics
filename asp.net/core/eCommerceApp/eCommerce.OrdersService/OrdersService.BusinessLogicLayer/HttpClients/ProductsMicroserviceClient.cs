using Microsoft.Extensions.Caching.Distributed;
using OrdersService.BusinessLogicLayer.DTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace OrdersService.BusinessLogicLayer.HttpClients
{
    public class ProductsMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _distributedCache;

        public ProductsMicroserviceClient(HttpClient httpClient, IDistributedCache distributedCache)
        {
            _httpClient = httpClient;
            _distributedCache = distributedCache;
        }

        public async Task<ProductDto?> GetProduct(Guid productId)
        {
            string cacheKey = $"product:{productId}";
            string? cacheProduct = await _distributedCache.GetStringAsync(cacheKey);
            if (cacheProduct != null)
            {
                var fromCache = JsonSerializer.Deserialize<ProductDto>(cacheProduct);
                return fromCache; // returns product from cache
            }

            HttpResponseMessage response = await _httpClient.GetAsync(
                $"/api/products/search/product-id/{productId}"
            );
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new HttpRequestException("Bad Request", null,
                        System.Net.HttpStatusCode.BadRequest);
                else throw new HttpRequestException(
                    $"Http request failed with status code {response.StatusCode}");
            }
            var product = await response.Content.ReadFromJsonAsync<ProductDto>();
            if (product == null) throw new HttpRequestException();

            var json = JsonSerializer.Serialize(product);

            await _distributedCache.SetStringAsync(cacheKey, json, new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
                .SetSlidingExpiration(TimeSpan.FromSeconds(20)));

            return product;
        }
    }
}
