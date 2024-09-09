using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.Models;

namespace TeaTime.Api.Services
{
    public class OrderService
    {
        private readonly TeaTimeContext _context;

        public OrderService(TeaTimeContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderDTO>> GetOrderDTO()
        {
            if (_context.Orders == null)
            {
                return Enumerable.Empty<OrderDTO>();
            }
            return await _context.Orders.Select(x => Order2DTO(x)).ToListAsync();
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
