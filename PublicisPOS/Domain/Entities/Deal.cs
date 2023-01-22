using PublicisPOS.Application.Services;
using PublicisPOS.Domain.Exceptions;
using PublicisPOS.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicisPOS.Domain.Entities
{
    public class Deal
    {
        public int Id { get; set; }
        public int Sku { get; set; }
        public DealType DealType { get; set; }
        public Discount Discount { get; set; }
        public Offer Offer { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DiscountType DiscountType { get; internal set; }
        public decimal DiscountValue { get; internal set; }
        public Deal(int Sku,DealType dealType, decimal discount, string offerDescription, int buyQuantity, int offerQuantity, DateTime starts, DateTime ends)
        {
            DealType = dealType;
            Discount = new Discount(discount);
            Offer = new Offer(offerDescription, buyQuantity, offerQuantity);
            StartDate = starts;
            EndDate = ends;
            
        }
        public Deal()
        {
            // ef needs this constructor even though it is never called by 
            // my code in the application. EF needs it to set up the contexts

            // Failure to have it will result in a 
            //  No suitable constructor found for entity type 'Company'. exception
        }
        public void ValidateDeal(Deal deal)
        {
            if (deal.StartDate > deal.EndDate)
            {
                throw new InvalidDealException("Invalid date range provided for the deal");
            }
            
            
            if (deal.DiscountType == DiscountType.Amount && (deal.Discount.Value<= 0 || deal.Discount.Value >= 100))
            {
                throw new InvalidDealException("Invalid fixed discount value provided for the deal");
            }
            if (DiscountType == DiscountType.Percentage && (deal.Discount.Value <= 0 || deal.Discount.Value > 100))
            {
                throw new InvalidDealException("Invalid percentage discount value provided for the deal");
            }
            
            if (deal.DealType.Type == DealType.DealTypeEnum.BuyTwoGetOneFree)
            {
                if (deal.Offer.BuyQuantity<1 )
                {
                    throw new InvalidDealException("Invalid buy quantity provided for the deal");
                }
                if (deal.Offer.OfferQuantity < 1)
                {
                    throw new InvalidDealException("Invalid buy quantity provided for the deal");
                }
            }
        }

    }

}