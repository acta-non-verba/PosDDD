using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.Domain.Aggregates
{
    public class Sale
    {
        public int Id { get; set; }
        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>() { };
        public List<Order> Orders { get; set; }
        public decimal Total { get; set; }

        public void ApplyDeals(List<Deal> deals)
        {
            foreach (var saleItem in SaleItems)
            {
                // apply deals to sale item
                foreach (var deal in deals)
                {
                    if (saleItem.Sku == deal.Sku)
                    {
                        if (deal.DealType.Type == DealType.DealTypeEnum.FlatDiscount)
                        {
                            saleItem.Discount = new Discount(deal.Discount.Value);
                        }
                        else if (deal.DealType.Type == DealType.DealTypeEnum.PercentageDiscount)
                        {
                            saleItem.Offer = deal.Offer;
                        }
                        else if (deal.DealType.Type == DealType.DealTypeEnum.BuyTwoGetOneFree)
                        {
                            saleItem.Offer = deal.Offer;
                        }
                    }
                }
            }
            // calculate total of sale items
            Total = SaleItems.Sum(x => x.Price - x.Discount.Value + x.Offer.OfferQuantity * x.Price);
        }

        public void CalculateEodTotal()
        {
            decimal subtotal = 0;
            foreach (var order in Orders)
            {
                subtotal += order.Total;
            }
            Total = subtotal + subtotal * 0.05m;
        }
    }

}
