using System.Text.Json;
using System.Text;
using TeaTime.Api.Domains.Store;

namespace TeaTime.Api.DataAccess.Repository
{
    public class APIStoreRepo : IStoresRepo
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        private static readonly JsonSerializerOptions _propertyNameCaseInsensitive = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public APIStoreRepo(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiSettings:BaseUrl"]!;

            // 增加 header
            var tokenHeader = configuration["ApiSettings:TokenHeader"]!;
            var secretToken = configuration["ApiSettings:SecretToken"]!;
            httpClient.DefaultRequestHeaders.Add(tokenHeader, secretToken);
        }

        public async Task<IEnumerable<Store>> GetStores()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/stores");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var stores = JsonSerializer.Deserialize<IEnumerable<Store>>(content, _propertyNameCaseInsensitive);


            return stores ?? Enumerable.Empty<Store>();

        }

        public async Task<Store?> GetStore(long id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/stores/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var store = JsonSerializer.Deserialize<Store>(content, _propertyNameCaseInsensitive);

            return store;
        }

        public async Task<Store?> PostStore(StoreDTO storeDTO)
        {
            var json = JsonSerializer.Serialize(storeDTO);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/stores", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var store = JsonSerializer.Deserialize<Store>(responseContent, _propertyNameCaseInsensitive);

            return store!;
        }

        public bool IsStoreExist()
        {
            var response = _httpClient.GetAsync($"{_baseUrl}/stores").Result;

            return response.IsSuccessStatusCode;
        }
        
    }
}
