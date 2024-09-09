using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess;
using TeaTime.Api.DataAccess.DBEntities;
using TeaTime.Api.Domains.Store;

namespace TeaTime.Api.Services
{
    public class StoreService:IStoreService
    {
        private readonly TeaTimeContext _context;
        private readonly ILogger<StoreService> _logger;

        public StoreService(TeaTimeContext context, ILogger<StoreService> logger)
        {
            _context = context;
            _logger = logger;
        }
        // GET: api/Stores
        public async Task<IEnumerable<Store>> GetStores()
        {
            if (_context.Stores == null)
            {
                _logger.LogWarning("目前沒有任何店家");
                return Enumerable.Empty<Store>();
            }
            return await _context.Stores.Select(x => Entity2Store(x)).ToListAsync();
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<Store?> GetStoreDTO(long id)
        {
            if (_context.Stores == null)
            {
                _logger.LogWarning("目前沒有任何店家");
                return null;
            }
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                _logger.LogWarning("商家代號 {storeId} 不存在", id);
                return null;
            }

            return Entity2Store(store);
        }



        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Store?> PostStoreDTO(StoreDTO storeDTO)
        {
            var store = new StoreEntity
            {
                Name = storeDTO.Name,
                PhoneNumber = storeDTO.PhoneNumber,
                MenuUrl = storeDTO.MenuUrl
            };

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return new Store
            {
                Name = store.Name,
                PhoneNumber = store.PhoneNumber,
                MenuUrl = store.MenuUrl
            };
        }


        private static Store Entity2Store(StoreEntity store) =>
           new Store
           {
               Id = store.Id,
               Name = store.Name,
               PhoneNumber = store.PhoneNumber,
               MenuUrl = store.MenuUrl
           };

    }
}
