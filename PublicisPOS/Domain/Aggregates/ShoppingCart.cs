using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.Domain.Aggregates
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public decimal Total { get; set; }

        public async Task ValidateCart(IInventoryService inventoryService)
        {
            foreach (var cartItem in CartItems)
            {
                // validate CartItem sku
                if (!(await cartItem.ValidateSku(inventoryService)))
                {
                    throw new InvalidOperationException("Invalid sku for cart item: " + cartItem.Sku);
                }
                // calculate total of Cart items
                Total += (decimal)cartItem.Price * cartItem.Quantity.Value;
            }
        }
    }
    public class CartItem
    {
        public int Sku { get; set; }
        public Quantity Quantity { get; set; }
        public double Price { get; set; }

        public void AddQuantity(int quantity)
        {
            Quantity.Value += quantity;
        }

        public void RemoveQuantity(int quantity)
        {
            Quantity.Value -= quantity;
        }

        public void RemoveItem()
        {
            Quantity.Value = 0;
        }
        public async Task<bool> ValidateSku(IInventoryService _inventoryService)
        {
            var item = await _inventoryService.GetInventoryItemBySkuAsync(Sku);
            return item != null && item.Quantity.Value >= Quantity.Value;
        }
    }

}
