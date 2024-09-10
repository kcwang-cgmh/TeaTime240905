using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess;
using TeaTime.Api.DataAccess.DBEntities;
using TeaTime.Api.DataAccess.Repository;
using TeaTime.Api.Domains.Order;

namespace TeaTime.Api.Services
{
    public class OrderService:IOrdersService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrdersRepo _orderRepo;

        public OrderService( ILogger<OrderService> logger, IOrdersRepo orderRepo) 
        {
            _logger = logger;
            _orderRepo = orderRepo;
        }

        // GET: api/stores/{storeId}/orders
        public async Task<IEnumerable<Order?>?> GetOrders(long storeId)
        {
            if (_orderRepo.HaveOrders() == false)
            {
                _logger.LogWarning("目前沒有任何訂單");
                return null;
            }
            var orders = await _orderRepo.GetOrders(storeId);
            if (orders == null)
            {
                _logger.LogWarning("不存在此商家代號 {storeId} ", storeId);
                return null;
            }
            if (!orders.Any())
            {
                _logger.LogWarning("商家代號 {storeId} 沒有存在任何訂單", storeId);
            }
            return orders;
        }

        //GET: api/stores/{storeId}/ orders /{id}
        public async Task<IEnumerable<Order?>> GetStoreOrder(long storeId, long id)
        {
            if (_orderRepo.HaveOrders() == false)
            {
                _logger.LogWarning("目前沒有任何訂單");
                return Enumerable.Empty<Order>();
            }
            var orders = await _orderRepo.GetStoreOrder(storeId, id);
            if (!orders.Any())
            {
                _logger.LogWarning("商家代號 {storeId} 沒有存在任何訂單", storeId);
            }
            return orders;

        }
        // POST: api / stores /{storeId}/ orders
        public async Task<Order?> PostOrder(long storeId, OrderDTO orderDTO)
        {
            var order = await _orderRepo.PostOrder(storeId, orderDTO);
            if (order is null)
            {
                _logger.LogWarning("商家代號 {storeId} 不存在", storeId);
                return null;
            }
            return order;
        }

    }
}
