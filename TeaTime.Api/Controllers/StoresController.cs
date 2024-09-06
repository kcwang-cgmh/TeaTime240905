using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeaTime.Api.Models;

namespace TeaTime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly TeaTimeContext _context;

        public StoresController(TeaTimeContext context)
        {
            _context = context;
        }

        // GET: api/Stores
        [HttpGet]
        public ActionResult<IEnumerable<Store>> GetStores()
        {
            var stores = _context.Stores;

            return Ok(stores);
        }

        // GET: api/Stores/1
        [HttpGet("{id}")]
        public ActionResult<Store> GetStore(long id)
        {
            var store = _context.Stores.Find(id);

            if (store is null)
            {
                return NotFound();
            }

            return Ok(store);
        }



        [HttpPost]
        public IActionResult AddStore([FromBody] Store newStore)
        {
            

            if (newStore == null)
            {
                return NotFound();
            }

            var maxId = _context.Stores.Any() ? _context.Stores.Max(s => s.Id) : 0;
            var newId = maxId + 1;
            newStore.Id = newId;

            _context.Add(newStore);
            _context.SaveChanges();

            return Ok();
        }

   


    }
}
