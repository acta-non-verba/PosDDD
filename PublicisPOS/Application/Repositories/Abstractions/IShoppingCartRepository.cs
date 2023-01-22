using PublicisPOS.Domain.Aggregates;

namespace PublicisPOS.Application.Repositories.Abstractions
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetShoppingCartById(int id);
        Task<ShoppingCart> Save(ShoppingCart cart);
    }
}
