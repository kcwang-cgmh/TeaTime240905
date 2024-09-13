using TeaTime.Api.Domain.Orders;


namespace TeaTime.Api.Services
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetOrders(long storeId);

        Order? GetOrder(long storeId, long id);

        Order? AddOrderAndReturn(long storeId, OrderForCreation newOrder);
    }
}
