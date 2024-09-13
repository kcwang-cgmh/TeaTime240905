using TeaTime.Api.Domain.Orders;

namespace TeaTime.Api.DataAccess.Repositories
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> GetOrders(long storeId);

        Order? GetOrder(long storeId, long id);

        Order AddOrderAndReturn(long storeId, OrderForCreation newOrder);
    }
}
