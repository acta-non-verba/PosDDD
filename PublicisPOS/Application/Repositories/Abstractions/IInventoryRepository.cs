using PublicisPOS.Domain.Entities;

namespace PublicisPOS.Application.Repositories.Abstractions
{
    public interface IInventoryRepository
    {
        InventoryItem GetBySKU(int Sku);
    }
}