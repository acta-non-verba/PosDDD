using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.Aggregates;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly IDealRepository _dealRepository;

        private Dictionary<DealType.DealTypeEnum, Func<SaleItem, Deal, decimal>> _pricingStrategies =
        new Dictionary<DealType.DealTypeEnum, Func<SaleItem, Deal, decimal>>()
            {
                { DealType.DealTypeEnum.FlatDiscount, (saleItem, deal) => FlatDealTypeCalculator(saleItem, deal) },
                { DealType.DealTypeEnum.PercentageDiscount, (saleItem, deal) => PercentageDealTypeCalculator(saleItem, deal) },
                { DealType.DealTypeEnum.BuyTwoGetOneFree, (saleItem, deal) => CalculateBuyNGetNBy2FreePrice(saleItem.Price, deal.Offer.BuyQuantity, deal.Offer.OfferQuantity) }
            };
        public SaleService(IDealRepository dealRepository)
        {
            _dealRepository = dealRepository;
        }

        public async void ApplyDeal(Sale sale)
        {
            foreach (var saleItem in sale.SaleItems)
            {
                var deal = await _dealRepository.GetDealBySKU(saleItem.Sku);
                if (deal != null)
                {
                    if (deal.StartDate <= DateTime.Now && deal.EndDate >= DateTime.Now)
                    {
                        if (_pricingStrategies.ContainsKey(deal.DealType.Type))
                        {
                            saleItem.Price = _pricingStrategies[deal.DealType.Type](saleItem, deal);
                        }
                    }
                }
                sale.Total += (saleItem.Price);
            }
        }

        private static decimal PercentageDealTypeCalculator(SaleItem saleItem, Deal deal)
        {
            var discount = (saleItem.Price * deal.Discount.Value) / 100;
            saleItem.Price -= discount;
            return saleItem.Price * saleItem.Quantity.Value;
        }

        private static decimal FlatDealTypeCalculator(SaleItem saleItem, Deal deal)
        {
            saleItem.Price -= deal.Discount.Value;
            return saleItem.Price * saleItem.Quantity.Value;
        }

        public static decimal CalculateBuyNGetNBy2FreePrice(decimal originalPrice, int buyQuantity, int offerQuantity)
        {
            int totalQuantity = buyQuantity + offerQuantity;
            int freeQuantity = totalQuantity / 2;
            int paidQuantity = totalQuantity - freeQuantity;
            return originalPrice * paidQuantity;
        }
        //public decimal CalculateFlatDiscountPrice(decimal originalPrice, decimal discount)
        //{
        //    return originalPrice - discount;
        //}

        //public decimal CalculatePercentageDiscountPrice(decimal originalPrice, decimal discount)
        //{
        //    return originalPrice - (originalPrice * (discount / 100));
        //}
    }

}


