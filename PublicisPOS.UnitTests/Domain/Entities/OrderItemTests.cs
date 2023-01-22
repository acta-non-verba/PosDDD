using Moq;
using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;

namespace PublicisPOS.UnitTests.Domain.Entities
{
    [TestClass]
    public class OrderItemTests
    {
        private Mock<IInventoryRepository> _inventoryRepositoryMock;
        private OrderItem _orderItem;

        [TestInitialize]
        public void TestInitialize()
        {
            _inventoryRepositoryMock = new Mock<IInventoryRepository>();
            _orderItem = new OrderItem(_inventoryRepositoryMock.Object);
        }

        [TestMethod]
        public void ValidateSKU_ReturnsTrue_WhenSKUExistsAndQuantityIsGreaterThanZero()
        {
            // Arrange
            _inventoryRepositoryMock.Setup(r => r.GetBySKU(453))
                .Returns(new InventoryItem {Sku=453, Quantity = new Quantity(15, Unit.Kilogram) });
            _orderItem.Sku = 453;

            // Act
            var result = _orderItem.ValidateSKU();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateSKU_ReturnsFalse_WhenSKUDoesNotExist()
        {
            // Arrange
            _inventoryRepositoryMock.Setup(r => r.GetBySKU(1)).Returns((InventoryItem)null);
            _orderItem.Sku = 1;

            // Act
            var result = _orderItem.ValidateSKU();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateSKU_ReturnsFalse_WhenQuantityIsZero()
        {
            // Arrange
            _inventoryRepositoryMock.Setup(r => r.GetBySKU(125))
                .Returns(new InventoryItem { Quantity = new Quantity(0, Unit.Gram) });
            _orderItem.Sku = 1;

            // Act
            var result = _orderItem.ValidateSKU();

            // Assert
            Assert.IsFalse(result);
        }
    }
}