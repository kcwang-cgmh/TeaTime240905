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
        private readonly StoreService _StoreService;

        public StoreController(TeaTimeContext context,ILogger<StoreService> logger)
        {
            _StoreService = new StoreService(context, logger);
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStoreDTO()
        {
            var stores = await _StoreService.GetStores();
            if (stores == null)
            {
                return NotFound("查無此筆資料");
            }
            return Ok(stores);
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStoreDTO(long id)
        {
            var store = await _StoreService.GetStoreDTO(id);
            if (store == null)
          {
              return NotFound("查無此筆資料");
          }
            return store;
        }

       

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Store>> PostStoreDTO(StoreDTO storeDTO)
        {
            var result = await _StoreService.PostStoreDTO(storeDTO);
            // 將storeDTO對應store
            return Ok(result);
        }
    }
}
