using Moq;
using PublicisPOS.Application.Repositories;
using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Application.Services;
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
    public class OrderTests
    {


        [TestMethod]
        public void ValidateOrder_ValidOrder_ShouldPass()
        {
            // Arrange


            var inventoryRepository = new Mock<IInventoryRepository>();
            var orderItems = new List<OrderItem>
            {
            new OrderItem(inventoryRepository.Object) { Sku = 799, Quantity = new Quantity(2,Unit.Number), Price = 10 }
            };
            var order = new Order { OrderItems = orderItems };
            inventoryRepository.Setup(x => x.GetBySKU(It.IsAny<int>())).Returns(new InventoryItem { Id = 2, Item = "Hair Ties", Price = 10.0m, Quantity = new Quantity(2, Unit.Number), Sku = 799 });
            // Act
            order.ValidateOrder();

            // Assert
            Assert.IsTrue(order.Total == 20);
        }

        [TestMethod]
        public void ValidateOrder_InvalidSKU_ShouldThrowException()
        {
            // Arrange
            var inventoryRepository = new Mock<IInventoryRepository>();
            var orderItems = new List<OrderItem>
            {
            new OrderItem(inventoryRepository.Object) { Sku = 1, Quantity = new Quantity(2,Unit.Number), Price = 10 },
            new OrderItem(inventoryRepository.Object){ Sku = 2, Quantity = new Quantity(2,Unit.Kilogram), Price = 20 }
            };
            var order = new Order { OrderItems = orderItems };
            inventoryRepository.Setup(x => x.GetBySKU(It.IsAny<int>())).Returns(new InventoryItem { Id = 1, Item = "Apples", Price = 1.0m, Quantity = new Quantity(2, Unit.Kilogram), Sku = 453 });
            // Act & Assert
            Assert.ThrowsException<InvalidOperationException>(() => order.ValidateOrder());
        }
    }

}
