using TeaTime.Api.Domains.Store;

namespace TeaTime.Api.Services
{
    public interface IStoreService
    {
        Task<IEnumerable<Store>> GetStores();
        Task<Store?> GetStoreDTO(long id);
        Task<Store?> PostStoreDTO(StoreDTO storeDTO);

    }
}
