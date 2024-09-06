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
            _context = context;
        }

        //GET: api/stores/{storeId}/orders
        [HttpGet("{storId}")]
        public ActionResult GetoOrders(long storeId)
        {
            var orders = _context.Orders.Find(storeId);

            if (orders is null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        //GET: api/stores/{storeId}/orders/{id}
        [HttpGet("{Id}")]
        public ActionResult GetoOrdersId(long Id)
        {
            var orders = _context.Orders.Find(Id);

            if (orders is null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        //POST: api/stores/{storeId}/orders
        [HttpPost]
        public IActionResult GetActionResultPost([FromBody] Order newOrder)
        {
            _context.Add(newOrder);
            _context.SaveChanges();

            return Ok();
        }
    }
}
