using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.Domain.Entities
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int Sku { get; set; }
        public Quantity Quantity { get; set; }
        public decimal Price { get; set; }
        public Discount Discount { get; set; }
        public Offer Offer { get; set; }

        public async Task<bool> ValidateSku(IInventoryService inventoryService)
        {
            var inventoryItem = await inventoryService.GetInventoryItemAsync(this.Sku);
            if (inventoryItem == null)
                return false;
            if (this.Quantity.Value > inventoryItem.Quantity.Value)
                return false;
            return true;
        }
    }

}