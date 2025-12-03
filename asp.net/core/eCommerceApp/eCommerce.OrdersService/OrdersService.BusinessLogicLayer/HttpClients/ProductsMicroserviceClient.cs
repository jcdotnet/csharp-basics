using OrdersService.BusinessLogicLayer.DTO;
using System.Net.Http.Json;

namespace OrdersService.BusinessLogicLayer.HttpClients
{
    public class ProductsMicroserviceClient
    {
        private readonly HttpClient _httpClient;

        public ProductsMicroserviceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto?> GetProduct(Guid productId)
        {
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
            return product;
        }
    }
}
