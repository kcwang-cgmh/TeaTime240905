using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeaTime.Api.Domain.Stores;
using TeaTime.Api.Services;

namespace TeaTime.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoresService _service;

        public StoresController(IStoresService service)
        {
            _service = service;
        }

        // GET: api/stores
        [HttpGet]
        public ActionResult<IEnumerable<Store>> GetStores()
        {
            var stores = _service.GetStores();

            return Ok(stores);
        }

        // GET: api/stores/1
        [HttpGet("{id}")]
        public ActionResult<Store> GetStore(long id)
        {
            var store = _service.GetStore(id);

            if (store is null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // POST: api/stores
        [HttpPost]
        public IActionResult AddStore(StoreForCreation newStore)
        {
            var storeForReturn = _service.AddStoreAndReturn(newStore);

            return CreatedAtAction(nameof(GetStore), new { id = storeForReturn.Id }, storeForReturn);
        }
    }
}
