namespace PublicisPOS.Application.Services.Abstractions
{
    public interface ICartItemService
    {
        double Price { get; set; }
        int Quantity { get; set; }
        int Sku { get; set; }

        void AddQuantity(int quantity);
        void RemoveItem();
        void RemoveQuantity(int quantity);
    }
}