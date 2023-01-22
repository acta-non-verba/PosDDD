using Microsoft.EntityFrameworkCore;
using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Domain.Aggregates;
using PublicisPOS.Infrastructure;

namespace PublicisPOS.Application.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShoppingCartContext _context;

        public ShoppingCartRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCart> GetShoppingCartById(int id)
        {
            return await _context.ShoppingCarts.Include(x => x.CartItems).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ShoppingCart> Save(ShoppingCart cart)
        {
            _context.ShoppingCarts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
    }

}
