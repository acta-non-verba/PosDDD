using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.ValueObjects;
using System;

namespace PublicisPOS.UnitTests.Domain.Entities
{
    [TestClass]
    public class InventoryItemTests
    {
        private Mock<InventoryItem> _mockInventoryItem;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockInventoryItem = new Mock<InventoryItem>();
        }

        [TestMethod]
        public void CalculatePrice_ReturnsCorrectPriceForKg()
        {
            //Arrange
            var quantity = new Quantity(2, Unit.Kilogram);
            _mockInventoryItem.Setup(x => x.Quantity).Returns(quantity);
            _mockInventoryItem.Setup(x => x.Price).Returns(50);
            //Act
            var result = _mockInventoryItem.Object.CalculatePrice();
            //Assert
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void CalculatePrice_ReturnsCorrectPriceForGm()
        {
            //Arrange
            var Quantity = new Quantity(200, Unit.Gram);
            _mockInventoryItem.Setup(x => x.Quantity).Returns(Quantity);
            _mockInventoryItem.Setup(x => x.Price).Returns(5);
            //Act
            var result = _mockInventoryItem.Object.CalculatePrice();
            //Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CalculatePrice_ReturnsCorrectPriceForNumber()
        {
            //Arrange
            var Quantity = new Quantity(5, Unit.Number);
            _mockInventoryItem.Setup(x => x.Quantity).Returns(Quantity);
            _mockInventoryItem.Setup(x => x.Price).Returns(50);
            //Act
            var result = _mockInventoryItem.Object.CalculatePrice();
            //Assert
            Assert.AreEqual(250, result);
        }
        [TestMethod]
        public void CalculatePrice_ThrowsExceptionForInvalidUnit()
        {
            //Arrange
            var Quantity = new Quantity(5, Unit.Invalid);
            _mockInventoryItem.Setup(x => x.Quantity).Returns(Quantity);
            _mockInventoryItem.Setup(x => x.Price).Returns(50);
            //Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => _mockInventoryItem.Object.CalculatePrice());
        }
    }

}