using Moq;
using PublicisPOS.Application.Services.Abstractions;
using PublicisPOS.Domain.Aggregates;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicisPOS.UnitTests.Domain.Entities
{
    [TestClass]
    public class SaleItemTests
    {
        private Mock<IInventoryService> _inventoryServiceMock;
        private SaleItem _saleItem;

        [TestInitialize]
        public void Initialize()
        {
            _inventoryServiceMock = new Mock<IInventoryService>();
            _saleItem = new SaleItem
            {
                Sku = 123,
                Quantity = new Quantity(2, It.IsAny<Unit>())
            };
        }

        [TestMethod]
        public async Task ValidateSku_ReturnsTrue_WhenInventoryItemExists()
        {
            // Arrange
            var inventoryItem = new InventoryItem
            {
                Sku = 123,
                Quantity = new Quantity(10, It.IsAny<Unit>())
            };
            _inventoryServiceMock.Setup(x => x.GetInventoryItemAsync(It.IsAny<int>())).ReturnsAsync(inventoryItem);

            // Act
            var result = await _saleItem.ValidateSku(_inventoryServiceMock.Object);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task ValidateSku_ReturnsFalse_WhenInventoryItemDoesNotExist()
        {
            // Arrange
            _inventoryServiceMock.Setup(x => x.GetInventoryItemAsync(It.IsAny<int>())).Returns(Task.FromResult((InventoryItem)null));

            // Act
            var result = await _saleItem.ValidateSku(_inventoryServiceMock.Object);

            // Assert
            Assert.IsFalse(result);
        }

    }

}
