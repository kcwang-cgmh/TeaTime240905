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
        private readonly TeaTimeContext _orderscontext;

        public OrdersController(TeaTimeContext context)
        {
            _orderscontext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            var orders = _orderscontext.Orders;

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(long id)
        {
            var orders = _orderscontext.Orders.Find(id);

            if (orders is null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] Order newStore)
        {
            newStore.Id = 0;
            _orderscontext.Add(newStore);
            _orderscontext.SaveChanges();

            return Ok();
        }
    }
}
