using Microsoft.EntityFrameworkCore;
using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Infrastructure;

namespace PublicisPOS.Application.Services
{
    public class InventoryService :  IInventoryService
    {
        private readonly InventoryContext _context;

        public InventoryService(InventoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryItem>> GetInventoryItemsAsync()
        {
            return await _context.InventoryItems.ToListAsync();
        }

        public async Task<InventoryItem> GetInventoryItemAsync(int id)
        {
            return await _context.InventoryItems.FindAsync(id);
        }

        public async Task<InventoryItem> AddInventoryItemAsync(InventoryItem inventoryItem)
        {
            await _context.InventoryItems.AddAsync(inventoryItem);
            await _context.SaveChangesAsync();
            return inventoryItem;
        }

        public async Task<InventoryItem> GetInventoryItemBySkuAsync(int sku)
        {
            return await _context.InventoryItems.FindAsync(sku);
        }
    }

}
