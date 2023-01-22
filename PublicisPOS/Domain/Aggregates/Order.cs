using PublicisPOS.Application.Services;
using PublicisPOS.Domain.Entities;

namespace PublicisPOS.Domain.Aggregates
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public decimal Total { get; set; }

        public void ValidateOrder()
        {
            foreach (var orderItem in OrderItems)
            {
                // validate OrderItem sku
                if (!orderItem.ValidateSKU())
                {
                    throw new InvalidOperationException("Invalid sku for order item: " + orderItem.Sku);
                }
                
                
            }
        }

        // calculate total of order items
        public void CalculateTotal()
        {
            Total = 0;
            foreach (var orderItem in OrderItems)
            {
                // calculate total of order items
                Total += ((decimal)orderItem.Price) * orderItem.Quantity.Value;
            }
        }
    }

}
