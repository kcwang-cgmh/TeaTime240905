
using TeaTime.Api.DataAccess.DBEntities;
using TeaTime.Api.Domains.Order;

namespace TeaTime.Api.Services
{
    public interface IOrderServicecs
    {
        Task<IEnumerable<Order>> GetOrder();
        Task<IEnumerable<Order>> GetOrders(long storeId);
        Task<IEnumerable<Order>> GetOrderWithId(long storeId, long id);
        Task<Order?> PostOrder(long storeId, OrderDTO orderDTO);
        
    }
}
