using Microsoft.EntityFrameworkCore;
using PublicisPOS.Domain.Aggregates;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PublicisPOS.Infrastructure
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().Property(x => x.Total).IsRequired();
            modelBuilder.Entity<Quantity>().HasNoKey();
            modelBuilder.Ignore<Quantity>();
            modelBuilder.Entity<OrderItem>().HasKey(x => x.Id);
            modelBuilder.Entity<OrderItem>().Property(x => x.Sku).IsRequired();
            modelBuilder.Entity<OrderItem>().Property(x => x.Quantity).IsRequired();

            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);
        }
    }

}
