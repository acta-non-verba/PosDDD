using Moq;
using PublicisPOS.Application.Repositories.Abstractions;
using PublicisPOS.Application.Services;
using PublicisPOS.Domain.Entities;
using PublicisPOS.Domain.Exceptions;
using PublicisPOS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicisPOS.UnitTests.Domain.Entities
{
    [TestClass]
    public class DealTests
    {
        [TestMethod]
        public void Test_ValidateDeal_InvalidDateRange_ThrowsInvalidDealException()
        {
            // Arrange
            var deal = new Deal(1, new DealType(DealType.DealTypeEnum.BuyTwoGetOneFree), 10, "Offer Description", 2, 1, DateTime.Now.AddDays(2), DateTime.Now);

            // Act & Assert
            Assert.ThrowsException<InvalidDealException>(() => deal.ValidateDeal(deal), "Invalid date range provided for the deal");
        }

        [TestMethod]
        public void Test_ValidateDeal_InvalidAmountDiscountValue_ThrowsInvalidDealException()
        {
            // Arrange
            var deal = new Deal(1, new DealType(DealType.DealTypeEnum.FlatDiscount), 110, "Offer Description", 2, 1, DateTime.Now, DateTime.Now.AddDays(2));

            // Act & Assert
            Assert.ThrowsException<InvalidDealException>(() => deal.ValidateDeal(deal), "Invalid fixed discount value provided for the deal");
        }

        [TestMethod]
        public void Test_ValidateDeal_InvalidPercentageDiscountValue_ThrowsInvalidDealException()
        {
            // Arrange
            var deal = new Deal(1, new DealType(DealType.DealTypeEnum.PercentageDiscount), 110, "Offer Description", 2, 1, DateTime.Now, DateTime.Now.AddDays(2));

            // Act & Assert
            Assert.ThrowsException<InvalidDealException>(() => deal.ValidateDeal(deal), "Invalid percentage discount value provided for the deal");
        }

        [TestMethod]
        public void Test_ValidateDeal_InvalidBuyQuantity_ThrowsInvalidDealException()
        {
            // Arrange
            var deal = new Deal(1, new DealType(DealType.DealTypeEnum.BuyTwoGetOneFree), 10, "Offer Description", 0, 1, DateTime.Now, DateTime.Now.AddDays(2));
            // Act & Assert
            Assert.ThrowsException<InvalidDealException>(() => deal.ValidateDeal(deal), "Invalid buy quantity provided for the deal");
        }

    }
}
