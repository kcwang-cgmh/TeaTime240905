using TeaTime.Api.Domain.Stores;

namespace TeaTime.Api.Services
{
    public interface IStoresService
    {
        IEnumerable<Store> GetStores();

        Store? GetStore(long id);

        Store AddStoreAndReturn(StoreForCreation newStore);

        bool IsStoreExist(long id);
    }
}
