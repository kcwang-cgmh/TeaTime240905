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
    [Route("api/store")]
    [ApiController]
    public class OrderDTOesController : ControllerBase
    {
        private readonly TeaTimeContext _context;

        public OrderDTOesController(TeaTimeContext context)
        {
            _context = context;
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

        /*// GET: api/OrderDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderDTO(long id)
        {
          if (_context.OrderDTO == null)
          {
              return NotFound();
          }
            var orderDTO = await _context.OrderDTO.FindAsync(id);

            if (orderDTO == null)
            {
                return NotFound();
            }

            return orderDTO;
        }

        // PUT: api/OrderDTOes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDTO(long id, OrderDTO orderDTO)
        {
            if (id != orderDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrderDTO(OrderDTO orderDTO)
        {
          if (_context.OrderDTO == null)
          {
              return Problem("Entity set 'TeaTimeContext.OrderDTO'  is null.");
          }
            _context.OrderDTO.Add(orderDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDTO", new { id = orderDTO.Id }, orderDTO);
        }

        // DELETE: api/OrderDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDTO(long id)
        {
            if (_context.OrderDTO == null)
            {
                return NotFound();
            }
            var orderDTO = await _context.OrderDTO.FindAsync(id);
            if (orderDTO == null)
            {
                return NotFound();
            }

            _context.OrderDTO.Remove(orderDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/
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
                return BadRequest("沒有這個店家哦!");
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
