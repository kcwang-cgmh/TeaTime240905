using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess;
using TeaTime.Api.DataAccess.DBEntities;
using TeaTime.Api.DataAccess.Repository;
using TeaTime.Api.Domains.Store;

namespace TeaTime.Api.Services
{
    public class StoreService:IStoresService
    {
        private readonly ILogger<StoreService> _logger;
        private readonly IStoresRepo _storeRepo;

        public StoreService(ILogger<StoreService> logger,IStoresRepo storeRepo)
        {
            _logger = logger;
            _storeRepo = storeRepo;
        }
        // GET: api/Stores
        public async Task<IEnumerable<Store>> GetStores()
        {
            if (_storeRepo.IsStoreExist() == false)
            {
                _logger.LogWarning("目前沒有任何店家");
                return Enumerable.Empty<Store>();
            }
            return await _storeRepo.GetStores();
        }

        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<Store?> GetStore(long id)
        {
            if (_storeRepo.IsStoreExist() == false)
            {
                _logger.LogWarning("目前沒有任何店家");
                return null;
            }
            var store = await _storeRepo.GetStore(id);

            if (store == null)
            {
                _logger.LogWarning("商家代號 {storeId} 不存在", id);
                return null;
            }

            return store;
        }

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Store?> PostStore(StoreDTO storeDTO)
        {
            return await _storeRepo.PostStore(storeDTO);
        }


    }
}
