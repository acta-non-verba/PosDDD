using Moq;
using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Application.Services;
using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;
using PublicisPOS.DTO;

namespace PublicisPOS.UnitTests.Application
{
    [TestClass]
    public class DealServiceTests
    {
        //private readonly IDealRepository _mockDealRepository;
        //private readonly IInventoryService _mockInventoryService;
        


        [TestMethod]
        public async Task TestCreateDeal_ReturnsDealDto()
        {

            // Arrange
            var _mockDealRepository = new Mock<IDealRepository>();
            var _mockInventoryService = new Mock<IInventoryService>();
            var _dealService = new DealService(_mockDealRepository.Object, _mockInventoryService.Object);
            var dealDto = new DealDto
            {
                Sku = 123,
                DealType = new DealType(DealType.DealTypeEnum.FlatDiscount),
                Discount = 10m,
                OfferDescription = "",
                BuyQuantity = 0,
                OfferQuantity = 0,
                Starts = DateTime.Now,
                Ends = DateTime.Now.AddDays(1)
            };
            _mockDealRepository.Setup(repo => repo.CreateDeal(It.IsAny<Deal>()))
                .ReturnsAsync(new Deal { Id = 1 });

            // Act
            var result = await _dealService.CreateDeal(dealDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dealDto.Sku, result.Sku);
            Assert.AreEqual(dealDto.DealType, result.DealType);
            Assert.AreEqual(dealDto.Discount, result.Discount);
            _mockDealRepository.Verify(repo => repo.CreateDeal(It.IsAny<Deal>()), Times.Once);
        }

        [TestMethod]
        public async Task TestCreateDeal_ThrowsException()
        {
            var _mockDealRepository = new Mock<IDealRepository>();
            var _mockInventoryService = new Mock<IInventoryService>();
            var _dealService = new DealService(_mockDealRepository.Object, _mockInventoryService.Object);
            // Arrange
            var dealDto = new DealDto
            {
                Sku = 123,
                DealType = new DealType(DealType.DealTypeEnum.FlatDiscount),
                Discount = 10m,
                OfferDescription = "",
                BuyQuantity = 0,
                OfferQuantity = 0,
                Starts = DateTime.Now,
                Ends = DateTime.Now.AddDays(1)
            };
            _mockDealRepository.Setup(repo => repo.CreateDeal(It.IsAny<Deal>()))
                .ReturnsAsync(new Deal { Id = 0 });

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _dealService.CreateDeal(dealDto));
            _mockDealRepository.Verify(repo => repo.CreateDeal(It.IsAny<Deal>()), Times.Once);
        }

        [TestMethod]
        public async Task TestGetAllDeals_ReturnsListOfDealDtos()
        {
            // Arrange
            var mockDealRepository = new Mock<IDealRepository>();
            var mockInventoryService = new Mock<IInventoryService>();
            var dealService = new DealService(mockDealRepository.Object, mockInventoryService.Object);

            var deal1 = new Deal(1, new DealType(DealType.DealTypeEnum.FlatDiscount), 10, "", 0, 0, new DateTime(2022, 1, 1), new DateTime(2022, 12, 31));
            var deal2 = new Deal(2, new DealType(DealType.DealTypeEnum.PercentageDiscount), 20, "", 0, 0, new DateTime(2022, 1, 1), new DateTime(2022, 12, 31));
            var deal3 = new Deal(3, new DealType(DealType.DealTypeEnum.BuyTwoGetOneFree), 0, "", 2, 1, new DateTime(2022, 1, 1), new DateTime(2022, 12, 31));
            deal1.Id = 1; deal2.Id = 2; deal3.Id = 3;
            var deals = new List<Deal> { deal1, deal2, deal3 };

            mockDealRepository.Setup(x => x.GetAllDeals()).Returns(Task.FromResult(deals));

            // Act
            var result = await dealService.GetAllDeals();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(DealType.DealTypeEnum.FlatDiscount, result[0].DealType.Type);
            Assert.AreEqual(10, result[0].Discount);
            Assert.AreEqual(2, result[2].BuyQuantity);
            Assert.AreEqual(1, result[2].OfferQuantity);
        }


    }
}
