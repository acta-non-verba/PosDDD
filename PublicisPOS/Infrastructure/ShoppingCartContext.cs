using Microsoft.EntityFrameworkCore;
using PublicisPOS.Domain.Aggregates;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.Infrastructure
{
    public class ShoppingCartContext : DbContext
    {
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCart>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Quantity>().HasNoKey();
            modelBuilder.Ignore<Quantity>();
            modelBuilder.Entity<ShoppingCart>()
                .HasMany(c => c.CartItems)
                .WithOne();

            modelBuilder.Entity<CartItem>()
                .HasKey(ci => ci.Sku);
        }
    }

}
