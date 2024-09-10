
using TeaTime.Api.DataAccess.DBEntities;
using TeaTime.Api.Domains.Order;

namespace TeaTime.Api.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<Order?>?> GetOrders(long storeId);
        Task<IEnumerable<Order?>> GetStoreOrder(long storeId, long id);
        Task<Order?> PostOrder(long storeId, OrderDTO orderDTO);

    }
}
