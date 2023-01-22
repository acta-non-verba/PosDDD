using PublicisPOS.Domain.Aggregates;

namespace PublicisPOS.Application.Services.Abstractions
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> Save(ShoppingCart cart);
    }
}