using Microsoft.EntityFrameworkCore;

namespace TeaTime.Api.Models
{
    public class TeaTimeContext : DbContext
    {
        public TeaTimeContext(DbContextOptions<TeaTimeContext> options) : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        //public DbSet<TeaTime.Api.Models.OrderDTO>? OrderDTO { get; set; }

        //public DbSet<TeaTime.Api.Models.StoreDTO>? StoreDTO { get; set; }
    }
}
