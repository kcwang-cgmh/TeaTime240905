using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess.DBEntities;
using TeaTime.Api.Domains.Store;

namespace TeaTime.Api.DataAccess.Repository
{
    public class InMemStoreRepo : IStoresRepo
    {
        private readonly TeaTimeContext _context;

        public InMemStoreRepo(TeaTimeContext context)
        {
            _context = context;
        }
        public async Task<Store?> GetStore(long id)
        {
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return null;
            }

            return Entity2Store(store);
        }

        public async Task<IEnumerable<Store>> GetStores()
        {
            return await _context.Stores.Select(x => Entity2Store(x)).ToListAsync();
        }

        public async Task<Store?> PostStore(StoreDTO storeDTO)
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
                Id = store.Id,
                Name = store.Name,
                PhoneNumber = store.PhoneNumber,
                MenuUrl = store.MenuUrl
            };
        }

        public bool IsStoreExist()
        {
            if (_context.Stores == null)
            {
                return false;
            }
            return true;
        }

        private static Store Entity2Store(StoreEntity store)
        {
            return new Store
            {
                Id = store.Id,
                Name = store.Name,
                PhoneNumber = store.PhoneNumber,
                MenuUrl = store.MenuUrl
            };
        }
    }
}
