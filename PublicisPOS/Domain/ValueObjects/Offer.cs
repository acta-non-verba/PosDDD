namespace PublicisPOS.Domain.ValueObjects
{
    public class Offer
    {
        public string Description { get; }
        public int BuyQuantity { get; }
        public int OfferQuantity { get; }

        public Offer(string description, int buyQuantity, int offerQuantity)
        {
            Description = description;
            BuyQuantity = buyQuantity;
            OfferQuantity = offerQuantity;
        }
    }

}
