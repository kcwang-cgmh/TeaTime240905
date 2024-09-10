using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess.DBEntities;
using TeaTime.Api.Domains.Order;

namespace TeaTime.Api.DataAccess.Repository
{
    public class InMemOrderRepo : IOrdersRepo
    {
        private readonly TeaTimeContext _context;

        public InMemOrderRepo(TeaTimeContext context) {
            this._context = context;
        }
        public async Task<IEnumerable<Order?>?> GetOrders(long storeId)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null)
            {
                return null;
            }
            var orders = await _context.Orders
                .Where(o => o.StoreId == storeId)
                .Select(x => Entity2Order(x))
                .ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<Order?>> GetStoreOrder(long storeId, long id)
        {
            var orders = await _context.Orders
                .Where(o => o.StoreId == storeId && o.Id == id)
                .Select(x => Entity2Order(x)).ToListAsync();
            return orders;
        }

        public bool HaveOrders()
        {
            if (_context.Orders == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Order?> PostOrder(long storeId, OrderDTO orderDTO)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store is null)
            {
                return null;
            }

            var order = new OrderEntity
            {
                StoreId = storeId,
                UserName = orderDTO.UserName,
                ItemName = orderDTO.ItemName,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new Order
            {
                Id = order.Id,
                UserName = order.UserName,
                ItemName = order.ItemName,
                Price = 0
            };
        }

        private static Order Entity2Order(OrderEntity order) =>
           new Order
           {
               Id = order.Id,
               UserName = order.UserName,
               ItemName = order.ItemName,
               Price = 0
           };
    }
}
