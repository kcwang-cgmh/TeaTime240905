using Microsoft.AspNetCore.Mvc;
using TeaTime.Api.Models;

namespace TeaTime.Api.Services
{
    public class OrdersService
    {

        public IEnumerable<Order> GetOrders(TeaTimeContext context, long storeId)
        {
            var store = GetStore(context, storeId);

            if (store == null)
            {
                return Enumerable.Empty<Order>();
            }

            var orders = context.Orders.Where(order => order.StoreId == storeId).ToList();
            
            return orders;
        }

        public Order GetOrder(TeaTimeContext context, long storeId, long orderId)
        {
            var store = GetStore(context, storeId);

            if (store == null)
            {
                return null;
            }

            var order = context.Orders.Where(order => order.StoreId == storeId && order.Id == orderId).FirstOrDefault();

            return order;
        }

        public void AddOrder(TeaTimeContext context, Order newOrder, long storeId)
        {
            var store = GetStore(context, storeId);

            if (store != null)
            {
                var orders = GetOrders(context, storeId);
                long biggest = 0;

                foreach (var order in orders)
                {
                    biggest = order.Id > biggest ? order.Id : biggest;
                }

                newOrder.StoreId = storeId;

                context.Add(newOrder);
                context.SaveChanges();
            }
            
        }


        public Store GetStore(TeaTimeContext context, long storeId)
        {
            return context.Stores.Find(storeId);
        }

    }
}
