using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Domain.Aggregates;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.Domain.Entities
{
    public class OrderItem
    {
        private readonly IInventoryRepository _inventoryRepository;
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int Sku { get; set; }
        public Quantity Quantity { get; set; }
        public double Price { get; set; }
        public virtual Order Order { get; set; }

        public OrderItem(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public bool ValidateSKU()
        {
            var item = _inventoryRepository.GetBySKU(Sku);
            return item != null && item.Quantity.Value > 0 && Sku==item.Sku;
        }
    }


}
