using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeaTime.Api.DataAccess;
using TeaTime.Api.DataAccess.DbEntity;
using TeaTime.Api.Domain.Stores;
using TeaTime.Api.Domain.StoresForUser;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores")]
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
        public ActionResult<IEnumerable<StoreEntity>> GetStores()
        {
            var results = _context.Stores.ToList();

            var stores = new List<Store>();
            foreach (var result in results)
            {
                stores.Add(new Store
                {
                    Id = result.Id,
                    Name = result.Name,
                    PhoneNumber = result.PhoneNumber,
                    MenuUrl = result.MenuUrl
                });
            }

            return Ok(stores);
        }

        // GET: api/Stores/1
        [HttpGet("{id}")]
        public ActionResult<StoreEntity> GetStore(long id)
        {
            var result = _context.Stores.Find(id);

            if (result == null)
            {
                return NotFound();
            }

            var store = new Store
            {
                Id = result.Id,
                Name = result.Name,
                PhoneNumber = result.PhoneNumber,
                MenuUrl = result.MenuUrl
            };

            return Ok(store);
        }

        // POST: api/Stores
        [HttpPost]
        public IActionResult AddStore([FromBody] StoreForUser newStore)
        { 
            var entity = new StoreEntity
            {
                Name = newStore.Name,
                PhoneNumber = newStore.PhoneNumber,
                MenuUrl = newStore.MenuUrl
            };

            _context.Stores.Add(entity);
            _context.SaveChanges();

            var storeForReturn = new Store
            {
                Id = entity.Id,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                MenuUrl = entity.MenuUrl
            };

            return CreatedAtAction(nameof(GetStore), new { id = entity.Id }, storeForReturn);
        }
    }
}
