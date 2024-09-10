using TeaTime.Api.Domains.Store;

namespace TeaTime.Api.Services
{
    public interface IStoresService
    {
        Task<IEnumerable<Store>> GetStores();
        Task<Store?> GetStore(long id);
        Task<Store?> PostStore(StoreDTO storeDTO);

    }
}
