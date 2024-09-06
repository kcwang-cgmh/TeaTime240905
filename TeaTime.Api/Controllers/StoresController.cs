using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeaTime.Api.Models;
using TeaTime.Api.Services;

namespace TeaTime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly TeaTimeContext _context;
        StoresService _service = new StoresService();

        public StoresController(TeaTimeContext context)
        {
            _context = context;
        }

        // GET: api/Stores
        [HttpGet]
        public ActionResult<IEnumerable<Store>> GetStores()
        {
            var stores = _service.GetStores(_context);

            return Ok(stores);
        }

        // GET: api/Stores/1
        [HttpGet("{id}")]
        public ActionResult<Store> GetStore(long id)
        {
            var store = _service.GetStore(_context, id);

            if (store is null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // POST: api/Stores
        [HttpPost]
        public IActionResult AddStore([FromBody] Store newStore)
        {
            _service.AddStore(_context, newStore);

            return Ok();
        }

    }
}
