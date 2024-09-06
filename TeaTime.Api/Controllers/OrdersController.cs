using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.Models;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores/{storeId}/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TeaTimeContext _context;
        public OrdersController(TeaTimeContext context)
        {
            _context = context;
        }
        // GET: api/stores/{storeId}/orders
        [HttpGet]
        public  ActionResult<IEnumerable<Order>> GetOrders()
        {
            var stores = _context.Stores;

            return Ok(stores);
        }

        // GET: api/stores/{storeId}/orders/{id}
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(long id)
        {
            var order = _context.Stores.Find(id);

            if (order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/stores/{storeId}/orders
        [HttpPost]
        public IActionResult AddOrder([FromBody] Order newOrder)
        {
            _context.Add(newOrder);
            _context.SaveChanges();

            return Ok();
        }
    }
}
