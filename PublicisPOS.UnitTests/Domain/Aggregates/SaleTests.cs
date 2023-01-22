using Moq;
using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Domain.Aggregates;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.UnitTests.Domain.Aggregates
{
    [TestClass]
    public class SaleTests
    {
        [TestMethod]
        public void ApplyDeals_ShouldApplyDealsToSaleItems()
        {
            // Arrange
            var mockInventoryRepository = new Mock<IInventoryRepository>();
            var deals = new List<Deal>
        {
            new Deal(1, new DealType(DealType.DealTypeEnum.FlatDiscount), 10, "", 0, 0, DateTime.Now, DateTime.Now.AddDays(1)),
            new Deal(2, new DealType(DealType.DealTypeEnum.PercentageDiscount), 20, "", 2, 1, DateTime.Now, DateTime.Now.AddDays(1)),
            new Deal(3, new DealType(DealType.DealTypeEnum.BuyTwoGetOneFree), 0, "", 2, 1, DateTime.Now, DateTime.Now.AddDays(1))
        };
            var saleItems = new List<SaleItem>
        {
            new SaleItem{Discount=new Discount(1.0m),Quantity=new Quantity(0,Unit.Number),Offer=new Offer("",0,0),Price=10.0m,Sku=453 },
            new SaleItem{Discount=new Discount(10.0m),Quantity=new Quantity(0,Unit.Number),Offer=new Offer("",0,0),Price=10.0m,Sku=125 },
            new SaleItem{Discount=new Discount(0.0m),Quantity=new Quantity(0,Unit.Number),Offer=new Offer("",2,1),Price=100.0m,Sku=799 },
        };
            var sale = new Sale { SaleItems = saleItems };

            // Act
            sale.ApplyDeals(deals);

            // Assert
            Assert.AreEqual(1.0m, saleItems[0].Discount.Value);
            Assert.AreEqual(0, saleItems[1].Offer.BuyQuantity);
            Assert.AreEqual(0, saleItems[1].Offer.OfferQuantity);
            Assert.AreEqual(2, saleItems[2].Offer.BuyQuantity);
            Assert.AreEqual(1, saleItems[2].Offer.OfferQuantity);
        }

        [TestMethod]
        public void CalculateEodTotal_ShouldCalculateTotalWithVAT()
        {
            // Arrange
            var orders = new List<Order>
        {
            new Order { Total = 100 },
            new Order { Total = 200 }
        };
            var sale = new Sale { Orders = orders };

            // Act
            sale.CalculateEodTotal();

            // Assert
            Assert.AreEqual(315m, sale.Total);
        }

    }
}
