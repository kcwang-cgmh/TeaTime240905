using Microsoft.EntityFrameworkCore;

namespace TeaTime.Api.Models
{
    public class TeaTimeContext : DbContext
    {
        public TeaTimeContext(DbContextOptions<TeaTimeContext> options) : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Store>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Order> Orders { get; set; } = null!;
    }
}
