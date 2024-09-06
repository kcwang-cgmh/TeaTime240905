using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.Models;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TeaTimeContext _context;
        public OrdersController(TeaTimeContext context)
        {
            _context= context;
        }
        //GET: {storeId}/orders

        [HttpGet("{storeId}/orders")]
        public ActionResult<IEnumerable<Order>> GetOrder(long storeId)
        {
            var orders = _context.Orders.Where(o => o.StoreId == storeId).ToList();
            var store = _context.Stores.Where(s => s.Id == storeId);
            if (store is null)
            {
                return NotFound();
            }
            if (orders is null)
            {
                return NotFound();
            }

            return Ok(orders);
        }
 
        //GET: {storeId}/orders/{id}
        [HttpGet("{storeId}/orders/{id}")]
        public ActionResult<Order> GetOrders(long storeId, long id)
        {
            var store = _context.Stores.Where(s => s.Id == storeId);
            if(store is null)
            {
                return NotFound();
            }
            var order = _context.Orders
                        .Where(o => o.StoreId == storeId && o.Id == id);
                        
            if (order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        //POST: {storeId}/orders
        [HttpPost("{storeId}/orders")]
        public IActionResult AddOrder(long storeId,[FromBody] Order newOrder)
        {
            var store = _context.Stores.Where(s => s.Id == storeId);
            if (store is null)
            {
                return NotFound();
            }
            newOrder.StoreId = storeId;
            _context.Add(newOrder);
            _context.SaveChanges();

            return Ok();
        }
    }
}
