using PublicisPOS.Application.Services.Abstractions;

namespace PublicisPOS.Application.Services
{
    public class CartItemService : ICartItemService
    {
        public int Sku { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void RemoveQuantity(int quantity)
        {
            Quantity -= quantity;
        }

        public void RemoveItem()
        {
            Quantity = 0;
        }
    }

}
