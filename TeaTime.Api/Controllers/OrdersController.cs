using Microsoft.AspNetCore.Mvc;
using TeaTime.Api.Models;
using TeaTime.Api.Services;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TeaTimeContext _context;
        OrdersService _ordersService = new OrdersService();

        public OrdersController(TeaTimeContext context)
        {
            _context = context;
        }

        // GET: api/stores/{storeId}/orders
        [HttpGet("{storeId}/orders")]
        public ActionResult<IEnumerable<Order>> GetOrders(long storeId)
        {
            var orders = _ordersService.GetOrders(_context, storeId);
            
            return Ok(orders);
        }

        // GET: api/stores/{storeId}/orders/{id}
        [HttpGet("{storeId}/orders/{orderId}")]
        public ActionResult<Order> GetOrder(long storeId, long orderId) 
        {
            var order = _ordersService.GetOrder(_context, storeId, orderId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/stores/{storeId}/orders
        [HttpPost("{storeId}/orders")]
        public IActionResult AddOrder([FromBody] Order newOrder, long storeId)
        {
            _ordersService.AddOrder(_context, newOrder, storeId);
            
            return Ok();
        }

    }
}
