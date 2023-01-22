using Microsoft.EntityFrameworkCore;
using PublicisPOS.Domain.Aggregates;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.Infrastructure
{
    public interface IShoppingCartContext      {
        DbSet<ShoppingCart> ShoppingCarts { get; set; }

    }
}