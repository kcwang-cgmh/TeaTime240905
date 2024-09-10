using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess.DBEntities;

namespace TeaTime.Api.DataAccess
{
    public class TeaTimeContext : DbContext
    {
        public TeaTimeContext(DbContextOptions<TeaTimeContext> options) : base(options)
        {
        }

        public DbSet<StoreEntity> Stores { get; set; } = null!;

        public DbSet<OrderEntity> Orders { get; set; } = null!;

    }
}
