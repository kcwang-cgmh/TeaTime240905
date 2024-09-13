using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.Domain.Orders;
using TeaTime.Api.Domain.Stores;
using TeaTime.Api.Services;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores/{storeId}/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        // GET: api/stores/{storeId}/orders
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders(long storeId)
        {
            var orders = _ordersService.GetOrders(storeId);

            return Ok(orders);
        }

        // GET: api/stores/{storeId}/orders/{id}
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(long storeId, long id)
        {
            var order = _ordersService.GetOrder(storeId, id);

            if (order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST: api/stores/{storeId}/orders
        [HttpPost]
        public IActionResult AddOrder(long storeId, [FromBody] OrderForCreation newOrder)
        {
            var orderForReturn = _ordersService.AddOrderAndReturn(storeId, newOrder);

            if (orderForReturn is null)
            {
                return BadRequest("無法新增訂單，請與維護人員聯繫");
            }

            return CreatedAtAction(nameof(GetOrder), new { storeId, id = orderForReturn.Id }, orderForReturn);
        }
    }
}
