using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachine.Core.Entities;
using SodaMachine.Core.Resources;

namespace SodaMachine.UnitTests
{
    [TestClass]
    public class SodaTests
    {
        [TestMethod]
        public void AddQuantity_AddValidAmountOfSoda_ReturnsValidString()
        {
            // Arrange
            var startQuantity = 2;
            var addedAmount = 2;
            var newQuantity = startQuantity + addedAmount;
            var sodaName = "Coke";
            var sodaPrice = 20;
            var soda = new Soda(sodaName, sodaPrice, startQuantity);

            // Act
            var message = soda.AddQuantity(addedAmount);

            // Assert
            Assert.IsTrue(soda.Name == sodaName);
            Assert.IsTrue(soda.Price == sodaPrice);
            Assert.IsTrue(soda.Quantity == newQuantity);
            Assert.AreEqual(message, string.Format(Messages.QuantityChanged, sodaName, newQuantity));
        }

        [TestMethod]
        public void AddQuantity_AddNotValidAmountOfSoda_ReturnsNotValidString()
        {
            // Arrange
            var startQuantity = 2;
            var addedAmount = -1;
            var sodaName = "Coke";
            var sodaPrice = 20;
            var soda = new Soda(sodaName, sodaPrice, startQuantity);

            // Act
            var message = soda.AddQuantity(addedAmount);

            // Assert
            Assert.IsTrue(soda.Name == sodaName);
            Assert.IsTrue(soda.Price == sodaPrice);
            Assert.IsTrue(soda.Quantity == startQuantity);
            Assert.AreEqual(message, string.Format(Messages.CantAddAmount, addedAmount));
        }

        [TestMethod]
        public void ChangePrice_AddValidPrice_ReturnsValidString()
        {
            // Arrange
            var startPrice = 20;
            var newPrice = 30;
            var sodaName = "Coke";
            var sodaQuantity = 5;
            var soda = new Soda(sodaName, startPrice, sodaQuantity);

            // Act
            var message = soda.ChangePrice(newPrice);

            // Assert
            Assert.IsTrue(soda.Name == sodaName);
            Assert.IsTrue(soda.Price == newPrice);
            Assert.IsTrue(soda.Quantity == sodaQuantity);
            Assert.AreEqual(message, string.Format(Messages.PriceChanged, sodaName, newPrice));
        }

        [TestMethod]
        public void ChangePrice_AddNotValidPrice_ReturnsNotValidString()
        {
            // Arrange
            var startPrice = 20;
            var newPrice = 0;
            var sodaName = "Coke";
            var sodaQuantity = 5;
            var soda = new Soda(sodaName, startPrice, sodaQuantity);

            // Act
            var message = soda.ChangePrice(newPrice);

            // Assert
            Assert.IsTrue(soda.Name == sodaName);
            Assert.IsTrue(soda.Price == startPrice);
            Assert.IsTrue(soda.Quantity == sodaQuantity);
            Assert.AreEqual(message, string.Format(Messages.PriceGreaterThan, 1));
        }

        [TestMethod]
        public void Buy_SuccessfulBuy_ReturnsSuccessfulString()
        {
            // Arrange
            var sodaPrice = 20;
            var sodaName = "Coke";
            var startQuantity = 5;
            var quantityAfterBuy = 4;

            var insertedMoney = 30;
            var moneyLeft = insertedMoney - sodaPrice;

            var soda = new Soda(sodaName, sodaPrice, startQuantity);

            // Act
            var message = soda.Buy(ref insertedMoney);

            // Assert
            Assert.IsTrue(soda.Name == sodaName);
            Assert.IsTrue(soda.Price == sodaPrice);
            Assert.IsTrue(soda.Quantity == quantityAfterBuy);
            Assert.IsTrue(insertedMoney == 0);
            Assert.AreEqual(message, $"{string.Format(Messages.GivingSodaOut, sodaName)}\n" +
                        $"{string.Format(Messages.GivingChangeOut, moneyLeft)}");
        }

        [TestMethod]
        public void Buy_QuantityIsZero_ReturnsNoSodaLeftString()
        {
            // Arrange
            var sodaPrice = 20;
            var sodaName = "Coke";
            var startQuantity = 0;

            var insertedMoney = 30;
            var moneyLeft = insertedMoney - sodaPrice;

            var soda = new Soda(sodaName, sodaPrice, startQuantity);

            // Act
            var message = soda.Buy(ref insertedMoney);

            // Assert
            Assert.IsTrue(soda.Name == sodaName);
            Assert.IsTrue(soda.Price == sodaPrice);
            Assert.IsTrue(soda.Quantity == startQuantity);
            Assert.IsTrue(insertedMoney != 0);
            Assert.AreEqual(message, string.Format(Messages.NoSodaLeft, sodaName));
        }

        [TestMethod]
        public void Buy_NotEnoughMoney_ReturnsNotEnoughMoneyString()
        {
            // Arrange
            var sodaPrice = 20;
            var sodaName = "Coke";
            var startQuantity = 5;
            var insertedMoney = 10;

            var moneyNeeded = sodaPrice - insertedMoney;

            var soda = new Soda(sodaName, sodaPrice, startQuantity);

            // Act
            var message = soda.Buy(ref insertedMoney);

            // Assert
            Assert.IsTrue(soda.Name == sodaName);
            Assert.IsTrue(soda.Price == sodaPrice);
            Assert.IsTrue(soda.Quantity == startQuantity);

            Assert.AreEqual(message, string.Format(Messages.NotEnoughMoney, moneyNeeded));
        }

        [TestMethod]
        public void SmsBuy_Success_ReturnsGivingSodaOutString()
        {
            // Arrange
            var sodaPrice = 20;
            var sodaName = "Coke";
            var startQuantity = 5;
            var quantityAfterBuy = 4;

            var soda = new Soda(sodaName, sodaPrice, startQuantity);

            // Act
            var message = soda.SmsBuy();

            // Assert
            Assert.IsTrue(soda.Name == sodaName);
            Assert.IsTrue(soda.Price == sodaPrice);
            Assert.IsTrue(soda.Quantity == quantityAfterBuy);

            Assert.AreEqual(message, string.Format(Messages.GivingSodaOut, sodaName));
        }

        [TestMethod]
        public void SmsBuy_NoSodaLeft_ReturnsNoSodaLeftString()
        {
            // Arrange
            var sodaPrice = 20;
            var sodaName = "Coke";
            var startQuantity = 0;

            var soda = new Soda(sodaName, sodaPrice, startQuantity);

            // Act
            var message = soda.SmsBuy();

            // Assert
            Assert.IsTrue(soda.Name == sodaName);
            Assert.IsTrue(soda.Price == sodaPrice);
            Assert.IsTrue(soda.Quantity == startQuantity);

            Assert.AreEqual(message, string.Format(Messages.NoSodaLeft, sodaName));
        }
    }
}
