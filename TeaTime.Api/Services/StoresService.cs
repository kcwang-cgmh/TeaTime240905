using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeaTime.Api.Models;

namespace TeaTime.Api.Services
{
    public class StoresService
    {
        public IEnumerable<Store> GetStores(TeaTimeContext context)
        {
            var stores = context.Stores;

            return stores;
        }

        public Store GetStore(TeaTimeContext context, long id)
        {
            var store = context.Stores.Find(id);

            return store;
        }

        public void AddStore(TeaTimeContext context, Store newStore)
        {
            var stores = GetStores(context);
            long biggest = 0;

            foreach (var store in stores)
            {
                biggest = store.Id > biggest ? store.Id : biggest;
            }

            newStore.Id = biggest + 1;

            context.Add(newStore);
            context.SaveChanges();

        }
    }
}
