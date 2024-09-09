using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess;
using TeaTime.Api.Domains.Order; // 與業務邏輯部分連接
using TeaTime.Api.Services;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores/{storeId}/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(TeaTimeContext context, ILogger<OrderService> logger)
        {
            _orderService = new OrderService(context, logger);
        }

        
        // GET: api/stores/{storeId}/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders(long storeId)
        {
            var orders = await _orderService.GetOrders(storeId);
            if (orders == null)
            {
                return NotFound("查無此筆資料");
            }
            return Ok(orders);
        }

        //GET: api/stores/{storeId}/ orders /{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrderWithId(long storeId, long id)
        {
            var order = await _orderService.GetOrderWithId(storeId,id);
            if (order == null)
            {
                return NotFound("查無此筆資料");
            }
            
            return Ok(order);

        }
        // POST: api / stores /{storeId}/ orders
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrder(long storeId, OrderDTO orderDTO)
        {
            var store = await _orderService.PostOrder(storeId, orderDTO);
            if (store is null)
            {
                return BadRequest("送出訂單失敗");
            }

            return Ok();
        }

    }
}
