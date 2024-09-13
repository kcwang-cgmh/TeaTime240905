using TeaTime.Api.Domain.Stores;

namespace TeaTime.Api.DataAccess.Repositories
{
    public interface IStoresRepository
    {
        IEnumerable<Store> GetStores();

        Store? GetStore(long id);

        Store AddStoreAndReturn(StoreForCreation newStore);

        bool IsStoreExist(long id);
    }
}
