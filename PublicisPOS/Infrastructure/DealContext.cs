using Microsoft.EntityFrameworkCore;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.Infrastructure
{
    public class DealContext : DbContext
    {
        public DealContext(DbContextOptions<DealContext> options) : base(options)
        {
        }

        public DbSet<Deal> Deals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deal>().HasKey(x => x.Id);

            modelBuilder.Entity<Quantity>().HasNoKey();
            modelBuilder.Ignore<Quantity>();

            modelBuilder.Entity<Discount>().HasNoKey();
            modelBuilder.Ignore<Discount>();

            modelBuilder.Entity<DealType>().HasNoKey();
            modelBuilder.Ignore<DealType>();

            modelBuilder.Entity<Offer>().HasNoKey();
            modelBuilder.Ignore<Offer>();

            modelBuilder.Entity<Deal>().Ignore(d => d.Discount);

            modelBuilder.Entity<Deal>().Property(x => x.Discount).IsRequired();
            modelBuilder.Entity<Deal>().Property(x => x.DiscountType).IsRequired();
            modelBuilder.Entity<Deal>().Property(x => x.Sku).IsRequired();
            modelBuilder.Entity<Deal>().Property(x => x.StartDate).IsRequired();
            modelBuilder.Entity<Deal>().Property(x => x.EndDate).IsRequired();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "DealsDb");
        }
    }

}
