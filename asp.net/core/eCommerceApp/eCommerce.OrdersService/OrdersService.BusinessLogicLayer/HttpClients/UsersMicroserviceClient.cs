using OrdersService.BusinessLogicLayer.DTO;
using System.Net.Http.Json;

namespace OrdersService.BusinessLogicLayer.HttpClients
{
    // custom http client service for users microservice
    public class UsersMicroserviceClient
    {
        private readonly HttpClient _httpClient;

        public UsersMicroserviceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto?> GetUser(Guid userId)
        {
            HttpResponseMessage response =  await _httpClient.GetAsync($"/api/users/{userId}");
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
            var user = await response.Content.ReadFromJsonAsync<UserDto>();
            if (user == null) throw new HttpRequestException();
            return user;
        }
    }
}
