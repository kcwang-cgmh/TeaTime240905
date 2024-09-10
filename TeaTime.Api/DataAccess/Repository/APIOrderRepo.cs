using TeaTime.Api.Domains.Order;

namespace TeaTime.Api.DataAccess.Repository
{
    public class APIOrderRepo:IOrdersRepo
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public APIOrderRepo(IConfiguration configuration, HttpClient httpClient)
        {
            this._configuration = configuration;
            this._httpClient = httpClient;
        }

        public Task<IEnumerable<Order?>?> GetOrders(long storeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order?>> GetStoreOrder(long storeId, long id)
        {
            throw new NotImplementedException();
        }

        public bool HaveOrders()
        {
            throw new NotImplementedException();
        }

        public Task<Order?> PostOrder(long storeId, OrderDTO orderDTO)
        {
            throw new NotImplementedException();
        }
    }
}
