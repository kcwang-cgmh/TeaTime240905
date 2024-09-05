using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.Models;

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
          if (_context.StoreDTO == null)
          {
              return NotFound();
          }
            return await _context.StoreDTO.ToListAsync();
        }

        // GET: api/StoreDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDTO>> GetStoreDTO(long id)
        {
          if (_context.StoreDTO == null)
          {
              return NotFound();
          }
            var storeDTO = await _context.StoreDTO.FindAsync(id);

            if (storeDTO == null)
            {
                return NotFound();
            }

            return storeDTO;
        }

        // PUT: api/StoreDTOes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoreDTO(long id, StoreDTO storeDTO)
        {
            if (id != storeDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(storeDTO).State = EntityState.Modified;

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
            // 將DTO對應
           var Store = new Store 
           { 
               Name = storeDTO.Name,
               PhoneNumber = storeDTO.PhoneNumber,
               MenuUrl = storeDTO.MenuUrl
           };
          if (_context.StoreDTO == null)
          {
              return Problem("Entity set 'TeaTimeContext.StoreDTO'  is null.");
          }
            _context.StoreDTO.Add(Store);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStoreDTO),
                new { id = Store.Id }, Store);
        }

        // DELETE: api/StoreDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreDTO(long id)
        {
            if (_context.StoreDTO == null)
            {
                return NotFound();
            }
            var storeDTO = await _context.StoreDTO.FindAsync(id);
            if (storeDTO == null)
            {
                return NotFound();
            }

            _context.StoreDTO.Remove(storeDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreDTOExists(long id)
        {
            return (_context.StoreDTO?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static StoreDTO StoreDTO(StoreDTO StoreDTO) =>
           new StoreDTO
           {
               Name = StoreDTO.Name,
               PhoneNumber = StoreDTO.PhoneNumber,
               MenuUrl = StoreDTO.MenuUrl
           };
    }
}
