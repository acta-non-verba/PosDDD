namespace PublicisPOS.Domain.ValueObjects
{
    public class Discount
    {
        public decimal Value { get; private set; }

        public Discount(decimal value)
        {
            Value = value;
        }

        
    }

}
