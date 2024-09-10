using TeaTime.Api.Domains.Store;

namespace TeaTime.Api.DataAccess.Repository
{
    public interface IStoresRepo
    {
        Task<IEnumerable<Store>> GetStores();
        Task<Store?> GetStore(long id);
        Task<Store?> PostStore(StoreDTO storeDTO);
        bool IsStoreExist();
    }
}
