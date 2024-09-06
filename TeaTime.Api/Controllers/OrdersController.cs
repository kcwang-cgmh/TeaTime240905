using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.Models;
using System.Linq;

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

        // GET api/stores/{storeId}/orders

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders(long storeId)
        {
            // 查詢指定商店的所有訂單
            var orders = _context.Orders
                .Where(o => o.StoreId == storeId)
                .ToList(); // 使用同步方法 ToList()

            if (orders == null || !orders.Any())
            {
                return NotFound(); // 如果找不到訂單，返回 404 Not Found
            }

            return Ok(orders); // 返回找到的訂單列表和 200 OK 狀態
        }

        // GET api/stores/{storeId}/orders/{id}
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(long storeId, long id)
        {
            // 查詢指定商店下的訂單
            var order = _context.Orders
                .FirstOrDefault(o => o.StoreId == storeId && o.Id == id);

            if (order == null)
            {
                return NotFound(); // 如果找不到訂單，返回 404 Not Found
            }

            return Ok(order); // 返回找到的訂單和 200 OK 狀態
        }

        [HttpPost]
        public IActionResult AddOrder(long storeId, [FromBody] Order newOrder)
        {


            if (newOrder == null)
            {
                return NotFound();
            }

            var store = _context.Stores.Find(storeId);
            if (store == null)
            {
                return NotFound("Store not found.");
            }

            var maxId = _context.Orders.Any() ? _context.Orders.Max(s => s.Id) : 0;
            var newId = maxId + 1;

            newOrder.StoreId = storeId;
            newOrder.Id = newId;

            _context.Add(newOrder);
            _context.SaveChanges();

            return Ok();
        }


    }
}
