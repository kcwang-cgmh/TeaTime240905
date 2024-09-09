using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess;
using TeaTime.Api.DataAccess.DBEntities;
using TeaTime.Api.Domains.Order;

namespace TeaTime.Api.Services
{
    public class OrderService:IOrderServicecs
    {
        private readonly TeaTimeContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(TeaTimeContext context, ILogger<OrderService> logger) 
        {
            _context = context; // 注入資料庫的表以查詢
            _logger = logger;
        }

        // GET: api/order
        public async Task<IEnumerable<Order>> GetOrder()
        {
            if (_context.Orders == null)
            {
                _logger.LogWarning("目前沒有任何訂單");
                return Enumerable.Empty<Order>();
            }
            return await _context.Orders.Select(x => Entity2Order(x)).ToListAsync();
        }

        // GET: api/stores/{storeId}/orders
        public async Task<IEnumerable<Order>> GetOrders(long storeId)
        {
            if (_context.Orders == null)
            {
                _logger.LogWarning("目前沒有任何訂單");
                return Enumerable.Empty<Order>();
            }
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null)
            {
                _logger.LogWarning("不存在此商家代號 {storeId} ", storeId);
                return Enumerable.Empty<Order>();
            }
            var orders = await _context.Orders
                .Where(o => o.StoreId == storeId)
                .Select(x => Entity2Order(x))
                .ToListAsync();
            if (orders.Count == 0)
            {
                _logger.LogWarning("商家代號 {storeId} 沒有存在任何訂單", storeId);
                return orders;
            }
            return orders;

        }

        //GET: api/stores/{storeId}/ orders /{id}
        public async Task<IEnumerable<Order>> GetOrderWithId(long storeId, long id)
        {
            if (_context.Orders == null)
            {
                _logger.LogWarning("目前沒有任何訂單");
                return Enumerable.Empty<Order>();
            }
            var orders = await _context.Orders
                .Where(o => o.StoreId == storeId && o.Id == id)
                .Select(x => Entity2Order(x))
                .ToListAsync();
            if (orders.Count == 0)
            {
                _logger.LogWarning("商家代號 {storeId} 沒有存在任何訂單", storeId);
                return orders;
            }
            return orders;

        }
        // POST: api / stores /{storeId}/ orders
        public async Task<Order?> PostOrder(long storeId, OrderDTO orderDTO)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store is null)
            {
                _logger.LogWarning("商家代號 {storeId} 沒有存在任何訂單", storeId);
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
