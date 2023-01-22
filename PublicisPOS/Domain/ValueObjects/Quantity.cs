using System.ComponentModel;

namespace PublicisPOS.Domain.ValueObjects
{
    public class Quantity
    {
        public decimal Value { get; set; }
        public Unit Unit { get; set; }
        public Quantity(decimal value, Unit unit)
        {
            Value = value;
            Unit = unit;
        }
    }

    public enum Unit
    {
        [Description("kg")]
        Kilogram,
        [Description("g")]
        Gram,
        [Description("")]
        Number,
        [Description("Invalid")]
        Invalid
    }
}
