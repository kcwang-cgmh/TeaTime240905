using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.Models;

namespace TeaTime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TeaTimeContext _context;
        public OrdersController(TeaTimeContext context)
        {
            _context= context;
        }
        //GET: api/stores/{storeId}/orders

        [HttpGet("api/stores/{storeId}/orders")]
        public ActionResult<IEnumerable<Order>> GetOrder(long storeId)
        {
            var orders = _context.Orders.Where(o => o.StoreId == storeId).ToList();

            if (orders is null)
            {
                return NotFound();
            }

            return Ok(orders);
        }
 
        //GET: api/stores/{storeId}/orders/{id}
        [HttpGet("api/stores/{storeId}/orders/{id}")]
        public ActionResult<Order> GetOrders(long storeId, long id)
        {
            var order = _context.Orders
                        .Where(o => o.StoreId == storeId && o.Id == id);
                        
            if (order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        //POST: api/stores/{storeId}/orders
        [HttpPost("api/stores/{storeId}/orders")]
        public IActionResult AddOrder(long storeId,[FromBody] Order newOrder)
        {
            newOrder.StoreId = storeId;
            _context.Add(newOrder);
            _context.SaveChanges();

            return Ok();
        }
    }
}
