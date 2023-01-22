using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.Infrastructure
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "InventoryDb");
        }

        public DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryItem>().HasKey(x => x.Sku);
            modelBuilder.Entity<Quantity>().HasNoKey();
            modelBuilder.Ignore<Quantity>();
            modelBuilder.Entity<InventoryItem>().Property(x => x.Sku).IsRequired();
            modelBuilder.Entity<InventoryItem>().Property(x => x.Item).IsRequired();
            modelBuilder.Entity<InventoryItem>().Property(x => x.Price).IsRequired();
        }
    }

}
