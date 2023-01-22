using PublicisPOS.Domain.Entities;

namespace PublicisPOS.Application.Services.Abstractions
{
    public interface IInventoryService
    {
        Task<InventoryItem> AddInventoryItemAsync(InventoryItem inventoryItem);
        Task<InventoryItem> GetInventoryItemAsync(int id);
        Task<InventoryItem> GetInventoryItemBySkuAsync(int sku);
        Task<IEnumerable<InventoryItem>> GetInventoryItemsAsync();
    }
}