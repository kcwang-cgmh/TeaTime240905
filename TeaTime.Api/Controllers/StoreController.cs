using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess;
using TeaTime.Api.Domains.Store;
using TeaTime.Api.Services;
using static NuGet.Packaging.PackagingConstants;

namespace TeaTime.Api.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoresService _storeService;

        public StoreController(IStoresService storeService)
        {
            _storeService = storeService;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        {
            var stores = await _storeService.GetStores();
            return Ok(stores);
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(long id)
        {
            var store = await _storeService.GetStore(id);
            if (store == null) { 
                return NotFound();
            }
            return Ok(store);
        }

       

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Store>> PostStoreDTO(StoreDTO storeDTO)
        {
            var result = await _storeService.PostStore(storeDTO);
            return Ok(result);
        }
    }
}
