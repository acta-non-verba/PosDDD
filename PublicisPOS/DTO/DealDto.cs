using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.DTO
{
    public class DealDto
    {
        public int Id { get; set; }
        public int Sku { get; set; }
        public DealType DealType { get; set; }
        public decimal Discount { get; set; }
        public string OfferDescription { get; set; }
        public int BuyQuantity { get; set; }
        public int OfferQuantity { get; set; }

        public DateTime Starts { get; set; } = DateTime.Now;
        public DateTime Ends{ get; set; }=DateTime.Now.AddDays(1);
    }
}
