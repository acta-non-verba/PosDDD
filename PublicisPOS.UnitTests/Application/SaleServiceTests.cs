using Moq;
using PublicisPOS.Application.Repositories;
using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Application.Services;
using PublicisPOS.Domain.Aggregates;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.UnitTests.Application
{
    #region old tests
    //[TestClass]
    //public class SaleServiceTests
    //{
    //    private Mock<IDealRepository> _dealRepositoryMock;
    //    private SaleService _saleService;
    //    private Deal _deal1;
    //    private Deal _deal2;
    //    private Sale _sale;
    //    private SaleItem _saleItem1;
    //    private SaleItem _saleItem2;
    //    private SaleItem _saleItem3;

    //    [TestInitialize]
    //    public void TestInitialize()
    //    {
    //        _dealRepositoryMock = new Mock<IDealRepository>();
    //        _saleService = new SaleService(_dealRepositoryMock.Object);
    //        _deal1 = new Deal(453,new DealType(DealType.DealTypeEnum.FlatDiscount), 1, "", 0, 0,DateTime.Today,DateTime.Today.AddDays(1).AddMinutes(-1));
    //        _deal2 = new Deal(799, new DealType(DealType.DealTypeEnum.PercentageDiscount), 10, "", 0, 0, DateTime.Today, DateTime.Today.AddDays(1).AddMinutes(-1));

    //        _sale = new Sale();
    //        _saleItem1 = new SaleItem() { Sku = 453, Quantity=new Quantity(2,Unit.Kilogram), Price = 75 };
    //        _saleItem2 = new SaleItem() { Sku = 799, Quantity = new Quantity(5, Unit.Number), Price = 15 };

    //        _sale.SaleItems.Add(_saleItem1);
    //        _sale.SaleItems.Add(_saleItem2);

    //    }

    //    [TestMethod]
    //    public void ApplyDeal_Should_Apply_Amount_Deal_To_SaleItem()
    //    {
    //        _dealRepositoryMock.Setup(x => x.GetDealBySKU(It.IsAny<int>())).ReturnsAsync(_deal1);
    //        _saleService.ApplyDeal(_sale);
    //        Assert.AreEqual(225, _sale.Total);
    //    }

    //    [TestMethod]
    //    public void ApplyDeal_Should_Apply_Percentage_Deal_To_SaleItem()
    //    {
    //        _deal2.Discount = new Discount(10);
    //        _deal2.DealType= new DealType(DealType.DealTypeEnum.PercentageDiscount);
    //        _dealRepositoryMock.Setup(x => x.GetDealBySKU(It.IsAny<int>())).ReturnsAsync(_deal2);
    //        _saleService.ApplyDeal(_sale);
    //        Assert.AreEqual(232, _sale.Total);
    //    }
    //    [TestMethod]
    //    public void ApplyDeal_Should_Not_Apply_Deal_If_Deal_Not_Found()
    //    {
    //        _dealRepositoryMock.Setup(x => x.GetDealBySKU(It.IsAny<int>())).ReturnsAsync(new Deal());
    //        _saleService.ApplyDeal(_sale);
    //        Assert.AreEqual(167, _sale.Total);
    //    }

    //    [TestMethod]
    //    public void ApplyDeal_Should_Not_Apply_Deal_If_Deal_Expired()
    //    {
    //        Deal _expiredDeal= new Deal(799, new DealType(DealType.DealTypeEnum.PercentageDiscount), 10, "", 0, 0, DateTime.Today.AddDays(-2), DateTime.Today.AddDays(-1));
    //        _dealRepositoryMock.Setup(x => x.GetDealBySKU(It.IsAny<int>())).ReturnsAsync(_deal1);
    //        _saleService.ApplyDeal(_sale);
    //        Assert.AreEqual(150, _sale.Total);
    //    }
    //}
    #endregion
    [TestClass]
    public class SaleServiceTests
    {
        private Mock<IDealRepository> _mockDealRepository;
        private SaleService _saleService;

        [TestInitialize]
        public void Initialize()
        {
            _mockDealRepository = new Mock<IDealRepository>();
            _saleService = new SaleService(_mockDealRepository.Object);
        }

        [TestMethod]
        public async Task ApplyDeal_AppliesFlatDiscountDealToSaleItem()
        {
            // Arrange
            var saleItem = new SaleItem() { Sku = 1, Price = 10, Quantity = new Quantity(1,Unit.Kilogram) };
            var deal = new Deal(1, new DealType(DealType.DealTypeEnum.FlatDiscount), 5, "", 0, 0, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            var sale = new Sale() { SaleItems = new List<SaleItem>() { saleItem } };
            _mockDealRepository.Setup(x => x.GetDealBySKU(saleItem.Sku)).Returns(Task.FromResult(deal));

            // Act
            _saleService.ApplyDeal(sale);

            // Assert
            Assert.AreEqual(5, saleItem.Price);
            Assert.AreEqual(5, sale.Total);
        }

        [TestMethod]
        public async Task ApplyDeal_AppliesPercentageDiscountDealToSaleItem()
        {
            // Arrange
            var saleItem = new SaleItem() { Sku = 1, Price = 10, Quantity = new Quantity(1,Unit.Gram) };
            var deal = new Deal(1, new DealType(DealType.DealTypeEnum.PercentageDiscount), 50, "", 0, 0, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            var sale = new Sale() { SaleItems = new List<SaleItem>() { saleItem } };
            _mockDealRepository.Setup(x => x.GetDealBySKU(saleItem.Sku)).Returns(Task.FromResult(deal));

            // Act
            _saleService.ApplyDeal(sale);

            // Assert
            Assert.AreEqual(5, saleItem.Price);
            Assert.AreEqual(5, sale.Total);
        }

        [TestMethod]
        public async Task ApplyDeal_AppliesBuyTwoGetOneFreeDealToSaleItem()
        {
            // Arrange
            var mockDealRepository = new Mock<IDealRepository>();
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now.AddDays(1);
            var deal = new Deal(1, new DealType(DealType.DealTypeEnum.BuyTwoGetOneFree), 0, "Buy 2 get 1 free", 2, 1, startDate, endDate);
            mockDealRepository.Setup(x => x.GetDealBySKU(1)).Returns(Task.FromResult(deal));
            var saleService = new SaleService(mockDealRepository.Object);
            var sale = new Sale
            {
                SaleItems = new List<SaleItem>
                            {
                                new SaleItem { Sku = 1, Quantity = new Quantity(3,Unit.Number), Price = 10 }
                            }
            };

            // Act
            saleService.ApplyDeal(sale);

            // Assert
            Assert.AreEqual(20, sale.Total);
            Assert.AreEqual(20, sale.SaleItems[0].Price);
        }

    }

}