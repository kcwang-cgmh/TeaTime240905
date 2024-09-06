using Microsoft.AspNetCore.Mvc;
using TeaTime.Api.DataAccess;
using TeaTime.Api.DataAccess.DbEntity;
using TeaTime.Api.Domain.Orders;
using TeaTime.Api.Domain.OrdersForUser;
using TeaTime.Api.Domain.Stores;
namespace TeaTime.Api.Controllers
{
    [Route("api/stores/{storeId}/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TeaTimeContext _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(TeaTimeContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //GET: api/stores/{storeId}/orders
        [HttpGet]
        public ActionResult GetOrders(long storeId)
        {
            var results = _context.Orders.Where(o => o.StoreId == storeId).ToList();

            var orders = new List<Order>();

            foreach (var result in results)
            {
                orders.Add(new Order
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    ItemName = result.ItemName
                });
            }

            return Ok(orders);
        }

        //GET: api/stores/{storeId}/orders/{id}
        [HttpGet("{Id}")]
        public ActionResult GetoOrder(long storeId, long id)
        {
            var store = _context.Stores.Find(storeId);
            if (store is null)
            {
                _logger.LogWarning("商家代號 {storeId} 不存在", storeId);
                return NotFound();
            }


            // 再檢查訂單是否存在且屬於該商家
            var result = _context.Orders.Find(id);

            if (result is null || result.StoreId != storeId)
            {
                _logger.LogWarning("訂單代號 {id} 不存在或不屬於商家", id);
                return NotFound();
            }

            var order = new Order
            {
                Id = result.Id,
                UserName = result.UserName,
                ItemName = result.ItemName,
                Price = 0 // TODO: 從商品資料表中取得價格
            };

            return Ok(order);
        }

        //POST: api/stores/{storeId}/orders
        [HttpPost]
        public IActionResult AddordersId(long storeId, [FromBody] OrderForUser newOrder)
        {
            // 先檢查商家是否存在
            var store = _context.Stores.Find(storeId);
            if (store is null)
            {
                _logger.LogWarning("商家代號 {storeId} 不存在，無法新增訂單", storeId);
                return BadRequest("無法新增訂單，請與維護人員聯繫");
            }

            var entity = new OrderEntity
            {
                StoreId = storeId,
                UserName = newOrder.UserName,
                ItemName = newOrder.ItemName
            };

            _context.Orders.Add(entity);
            _context.SaveChanges();

            var orderForReturn = new Order
            {
                Id = entity.Id,
                UserName = entity.UserName,
                ItemName = entity.ItemName,
                Price = 0 // TODO: 從商品資料表中取得價格
            };

            return CreatedAtAction(nameof(GetOrders), new { storeId, id = entity.Id }, orderForReturn);
        }
    }
}
