using Microsoft.EntityFrameworkCore;
using TeaTime.Api.DataAccess.DbEntity;

namespace TeaTime.Api.DataAccess
{
    public class TeaTimeContext : DbContext
    {
        public TeaTimeContext(DbContextOptions<TeaTimeContext> options) : base(options)
        {
        }

        public DbSet<StoreEntity> Stores { get; set; } = null!;

        public DbSet<OrderEntity> Orders { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreEntity>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<StoreEntity>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<OrderEntity>()
               .HasKey(s => s.Id);
            modelBuilder.Entity<OrderEntity>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
        }

    }
}
