using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.Models;

namespace TeaTime.Api.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly TeaTimeContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(TeaTimeContext context, ILogger<OrderController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/OrderDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrderDTO()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            return await _context.Orders.Select(x => Order2DTO(x)).ToListAsync();
        }

        
        // GET: api/stores/{storeId}/orders
        [HttpGet("{storeId}/orders")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersDTO(long storeId)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var store = await _context.Stores.FindAsync(storeId);
            if (store == null)
            {
                return NotFound();
            }
            var orders = await _context.Orders
                .Where(o => o.StoreId == storeId)
                .Select(x => Order2DTO(x))
                .ToListAsync();
            if (orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        //GET: api/stores/{storeId}/ orders /{id}
        [HttpGet("{storeId}/orders/{id}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrderDTO(long storeId, long id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var orders = await _context.Orders
                .Where(o => o.StoreId == storeId && o.Id == id)
                .Select(x => Order2DTO(x))
                .ToListAsync();
            if (orders.Count == 0)
            {
                return NotFound();
            }
            return Ok(orders);

        }
        // POST: api / stores /{storeId}/ orders
        [HttpPost("{storeId}/orders")]
        public async Task<ActionResult<OrderDTO>> PostOrderDTO(long storeId, OrderDTO orderDTO)
        {
            var store = await _context.Stores.FindAsync(storeId);
            if (store is null)
            {
                return BadRequest();
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

            return CreatedAtAction(nameof(GetOrderDTO),
                new { id = order.Id }, Order2DTO(order));
        }

        private bool OrderDTOExists(long id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
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
