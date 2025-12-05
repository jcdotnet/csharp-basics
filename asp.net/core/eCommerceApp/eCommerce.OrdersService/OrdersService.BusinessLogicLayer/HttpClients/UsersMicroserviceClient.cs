using Microsoft.Extensions.Caching.Distributed;
using OrdersService.BusinessLogicLayer.DTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace OrdersService.BusinessLogicLayer.HttpClients
{
    public class UsersMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _distributedCache;

        public UsersMicroserviceClient(HttpClient httpClient, IDistributedCache distributedCache)
        {
            _httpClient = httpClient;
            _distributedCache = distributedCache;
        }

        public async Task<UserDto?> GetUser(Guid userId)
        {
            string cacheKey = $"user:{userId}";
            string? cacheUser = await _distributedCache.GetStringAsync(cacheKey);
            if (cacheUser != null)
            {
                var fromCache = JsonSerializer.Deserialize<UserDto>(cacheUser);
                return fromCache; // returns user from cache
            }

            HttpResponseMessage response =  await _httpClient.GetAsync($"/gateway/users/{userId}");
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

            var json = JsonSerializer.Serialize(user);

            await _distributedCache.SetStringAsync(cacheKey, json, new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
                .SetSlidingExpiration(TimeSpan.FromSeconds(20)));
            return user;
        }
    }
}
