using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.Models;
using TeaTime.Api.Services;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(TeaTimeContext context, ILogger<OrderService> logger)
        {
            _orderService = new OrderService(context, logger);
        }


        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrderDTO()
        {
            var order = await _orderService.GetOrder();
            if (order == null)
          {
              return NotFound("查無此筆資料");
          }
            return Ok(order);
        }

        
        // GET: api/stores/{storeId}/orders
        [HttpGet("{storeId}/orders")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersDTO(long storeId)
        {
            var orders = await _orderService.GetOrders(storeId);
            if (orders == null)
            {
                return NotFound("查無此筆資料");
            }
            return Ok(orders);
        }

        //GET: api/stores/{storeId}/ orders /{id}
        [HttpGet("{storeId}/orders/{id}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrderDTO(long storeId, long id)
        {
            var order = await _orderService.GetOrderWithId(storeId,id);
            if (order == null)
            {
                return NotFound("查無此筆資料");
            }
            
            return Ok(order);

        }
        // POST: api / stores /{storeId}/ orders
        [HttpPost("{storeId}/orders")]
        public async Task<ActionResult<OrderDTO>> PostOrderDTO(long storeId, OrderDTO orderDTO)
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
