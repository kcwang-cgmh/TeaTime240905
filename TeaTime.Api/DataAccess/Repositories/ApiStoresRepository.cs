using System.Text.Json;
using System.Text;
using TeaTime.Api.Domain.Stores;
using TeaTime.Api.Domain.Orders;

namespace TeaTime.Api.DataAccess.Repositories
{
    public class ApiStoresRepository : IStoresRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        private static readonly JsonSerializerOptions _propertyNameCaseInsensitive = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiStoresRepository(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"]!;

            // 增加 header
            var tokenHeader = configuration["ApiSettings:TokenHeader"]!;
            var secretToken = configuration["ApiSettings:SecretToken"]!;
            httpClient.DefaultRequestHeaders.Add(tokenHeader, secretToken);
        }

        public IEnumerable<Store> GetStores()
        {
            var response = _httpClient.GetAsync($"{_baseUrl}/stores").Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var stores = JsonSerializer.Deserialize<IEnumerable<Store>>(content, _propertyNameCaseInsensitive);

            if (stores is not null)
            {
                return stores;
            }

            return Enumerable.Empty<Store>();
        }

        public Store? GetStore(long id)
        {
            var response = _httpClient.GetAsync($"{_baseUrl}/stores/{id}").Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var store = JsonSerializer.Deserialize<Store>(content, _propertyNameCaseInsensitive);

            return store;
        }

        public Store AddStoreAndReturn(StoreForCreation newStore)
        {
            var json = JsonSerializer.Serialize(newStore);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync($"{_baseUrl}/stores", content).Result;
            response.EnsureSuccessStatusCode();

            var responseContent = response.Content.ReadAsStringAsync().Result;
            var store = JsonSerializer.Deserialize<Store>(responseContent, _propertyNameCaseInsensitive);

            return store!;
        }

        public bool IsStoreExist(long id)
        {
            var response = _httpClient.GetAsync($"{_baseUrl}/stores/{id}").Result;

            return response.IsSuccessStatusCode;
        }
    }
}
