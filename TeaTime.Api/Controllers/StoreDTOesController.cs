using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.Models;
using static NuGet.Packaging.PackagingConstants;

namespace TeaTime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreDTOesController : ControllerBase
    {
        private readonly TeaTimeContext _context;

        public StoreDTOesController(TeaTimeContext context)
        {
            _context = context;
        }

        // GET: api/StoreDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreDTO>>> GetStoreDTO()
        {
          if (_context.Stores == null)
          {
              return NotFound();
          }
            return await _context.Stores.Select(x => Store2DTO(x)).ToListAsync();
        }

        // GET: api/StoreDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDTO>> GetStoreDTO(long id)
        {
          if (_context.Stores == null)
          {
              return NotFound();
          }
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return Store2DTO(store);
        }

        // GET: api/stores/{storeId}/orders
        [HttpGet("{storeId}/orders")]
        public async Task<ActionResult<OrderDTO>> GetOrderDTO(long storeId)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(storeId);
            if (order == null)
            {
                return NotFound();
            }
            return Order2DTO(order);

        }

        // GET: api/stores/{storeId}/ orders /{id}
        //[HttpGet("{storeId}/ orders /{id}")]
        //public async Task<ActionResult<OrderDTO>> GetOrderDTO(long storeId)
        //{
        //    if (_context.Orders == null)
        //    {
        //        return NotFound();
        //    }
        //    var order = await _context.Orders.FindAsync(storeId);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    return Order2DTO(order);

        //}

        // PUT: api/StoreDTOes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoreDTO(long id, Store store)
        {
            if (id != store.Id)
            {
                return BadRequest();
            }

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StoreDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoreDTO>> PostStoreDTO(StoreDTO storeDTO)
        {
            // 將storeDTO對應store
           var store = new Store 
           { 
               Name = storeDTO.Name,
               PhoneNumber = storeDTO.PhoneNumber,
               MenuUrl = storeDTO.MenuUrl
           };

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStoreDTO),
                new { id = store.Id }, Store2DTO(store));
        }

        // POST: api / stores /{storeId}/ orders
        [HttpPost("{storeId}/orders")]
        public async Task<ActionResult<OrderDTO>> PostOrderDTO(long storeId, OrderDTO orderDTO)
        {
            var store =  await _context.Stores.FindAsync(storeId);
            if (store is null)
            {
                return BadRequest(); 
            } 

            var order = new Order
            {
                StoreId = storeId,
                UserName = orderDTO.UserName,
                ItemName = orderDTO.ItemName,
                Price = orderDTO.Price
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStoreDTO),
                new { id = order.Id }, Order2DTO(order));
        }
        // DELETE: api/StoreDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreDTO(long id)
        {
            if (_context.Stores == null)
            {
                return NotFound();
            }
            var storeDTO = await _context.Stores.FindAsync(id);
            if (storeDTO == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(storeDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreDTOExists(long id)
        {
            return (_context.Stores?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static StoreDTO Store2DTO(Store store) =>
           new StoreDTO
           {
               Id = store.Id,
               Name = store.Name,
               PhoneNumber = store.PhoneNumber,
               MenuUrl = store.MenuUrl
           };

        private static OrderDTO Order2DTO(Order order) =>
           new OrderDTO
           {
               Id = order.Id,
               StoreId = order.StoreId,
               UserName = order.UserName,
               ItemName = order.ItemName,
               Price = order.Price
           };
    }
}
