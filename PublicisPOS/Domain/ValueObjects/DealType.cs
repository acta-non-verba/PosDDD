namespace PublicisPOS.Domain.ValueObjects
{
    public class DealType
    {
        public DealTypeEnum Type { get; }

        public DealType(DealTypeEnum type)
        {
            Type = type;
        }

        public enum DealTypeEnum
        {
            FlatDiscount,
            PercentageDiscount,
            BuyTwoGetOneFree
        }
    }


}
