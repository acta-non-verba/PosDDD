using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.Domain.Entities
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public int Sku { get; set; }
        public string Item { get; set; }

        public virtual Quantity Quantity { get; set; }
        public virtual decimal Price { get; set; }

        public decimal CalculatePrice()
        {
            decimal finalPrice = 0;
            switch (Quantity.Unit)
            {
                case Unit.Kilogram:
                    finalPrice = Quantity.Value * Price;
                    break;
                case Unit.Gram:
                    finalPrice = (Quantity.Value / 1000) * Price;
                    break;
                case Unit.Number:
                    finalPrice = Quantity.Value * Price;
                    break;
                default:
                    throw new InvalidOperationException("Invalid unit of measurement provided");
            }
            return finalPrice;
        }
    }

    

}
