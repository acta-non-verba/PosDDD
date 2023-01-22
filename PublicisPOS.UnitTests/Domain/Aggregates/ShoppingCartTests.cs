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

namespace PublicisPOS.UnitTests.Domain.Aggregates
{
    [TestClass]
    public class ShoppingCartTests
    {
        [TestMethod]
        public async Task ValidateCart_InvalidSku_ThrowsException()
        {
            // Arrange
            var cart = new ShoppingCart()
            {
                CartItems = new List<CartItem>(){new CartItem() { Sku = 123, Quantity = new Quantity(5,Unit.Number), Price = 10 }
            }
            };
            var inventoryServiceMock = new Mock<IInventoryService>();
            inventoryServiceMock.Setup(x => x.GetInventoryItemBySkuAsync(It.IsAny<int>())).Returns(Task.FromResult(new InventoryItem { Sku = -99, Quantity = new Quantity(1, Unit.Number) }));

            // Act
            try
            {
                await cart.ValidateCart(inventoryServiceMock.Object);
            }
            catch (InvalidOperationException ex)
            {
                // Assert
                Assert.AreEqual("Invalid sku for cart item: 123", ex.Message);
                return;
            }
            Assert.Fail("Expected exception was not thrown");
        }
    }
}
