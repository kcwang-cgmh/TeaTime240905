using TeaTime.Api.Domains.Order;

namespace TeaTime.Api.DataAccess.Repository
{
    public interface IOrdersRepo
    {
        Task<IEnumerable<Order?>?> GetOrders(long storeId); //null或空集合
        Task<IEnumerable<Order?>> GetStoreOrder(long storeId, long id);// 空集合
        Task<Order?> PostOrder(long storeId, OrderDTO orderDTO);
        bool HaveOrders();
    }
}
