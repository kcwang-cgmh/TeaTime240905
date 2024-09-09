using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess;
using TeaTime.Api.Models.Order;
namespace TeaTime.Api.Services
{
    public class OrderService
    {
        private readonly TeaTimeContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(TeaTimeContext context, ILogger<OrderService> logger) 
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/order
        public async Task<IEnumerable<OrderDTO>> GetOrder()
        {
            if (_context.Orders == null)
            {
                _logger.LogWarning("目前沒有任何訂單");
                return Enumerable.Empty<OrderDTO>();
            }
            return await _context.Orders.Select(x => Order2DTO(x)).ToListAsync();
        }

        // GET: api/stores/{storeId}/orders
        public async Task<IEnumerable<OrderDTO>> GetOrders(long storeId)
        {
            if (_context.Orders == null)
            {
                _logger.LogWarning("目前沒有任何訂單");
                return Enumerable.Empty<OrderDTO>();
            }
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null)
            {
                _logger.LogWarning("不存在此商家代號 {storeId} ", storeId);
                return Enumerable.Empty<OrderDTO>();
            }
            var orders = await _context.Orders
                .Where(o => o.StoreId == storeId)
                .Select(x => Order2DTO(x))
                .ToListAsync();
            if (orders.Count == 0)
            {
                _logger.LogWarning("商家代號 {storeId} 沒有存在任何訂單", storeId);
                return orders;
            }
            return orders;

        }

        //GET: api/stores/{storeId}/ orders /{id}
        public async Task<IEnumerable<OrderDTO>> GetOrderWithId(long storeId, long id)
        {
            if (_context.Orders == null)
            {
                _logger.LogWarning("目前沒有任何訂單");
                return Enumerable.Empty<OrderDTO>();
            }
            var orders = await _context.Orders
                .Where(o => o.StoreId == storeId && o.Id == id)
                .Select(x => Order2DTO(x))
                .ToListAsync();
            if (orders.Count == 0)
            {
                _logger.LogWarning("商家代號 {storeId} 沒有存在任何訂單", storeId);
                return orders;
            }
            return orders;

        }
        // POST: api / stores /{storeId}/ orders
        public async Task<OrderDTO?> PostOrder(long storeId, OrderDTO orderDTO)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store is null)
            {
                _logger.LogWarning("商家代號 {storeId} 沒有存在任何訂單", storeId);
                return null;
            }

            var order = new Order
            {
                StoreId = storeId,
                UserName = orderDTO.UserName,
                ItemName = orderDTO.ItemName,
                Price = orderDTO.Price
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new OrderDTO
            {
                UserName = order.UserName,
                ItemName = order.ItemName,
                Price = order.Price
            };
        }

        private static OrderDTO Order2DTO(Order order) =>
           new OrderDTO
           {
               Id = order.Id,
               StoreId = order.StoreId,
               UserName = order.UserName,
               ItemName = order.ItemName,
               Price = order.Price
           };
    }
}
