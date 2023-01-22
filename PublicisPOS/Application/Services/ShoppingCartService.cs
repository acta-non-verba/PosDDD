using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.Aggregates;

namespace PublicisPOS.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IInventoryService _inventoryService;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IInventoryService inventoryService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _inventoryService = inventoryService;
        }

        public async Task<ShoppingCart> Save(ShoppingCart cart)
        {
            cart.ValidateCart(_inventoryService);
            var savedCart = await _shoppingCartRepository.Save(cart);
            return savedCart;
        }
    }

}
