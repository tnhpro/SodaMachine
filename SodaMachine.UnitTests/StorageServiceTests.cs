using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachine.Core.Entities;
using SodaMachine.Core.Services;

namespace SodaMachine.UnitTests
{
    [TestClass]
    public class StorageServiceTests
    {
        private IStorageService _storageService;

        [TestInitialize]
        public void SetUp()
        {
            _storageService = new StorageService
            (
                new List<Soda>
                {
                    new Soda("coke", 20, 5),
                    new Soda("fanta", 10, 2),
                }
            );
        }

        [TestMethod]
        public void AddInventory_AddingNewSoda_ReturnsTrue()
        {
            // Arrange
            var newSodaName = "solo";
            var newSodaPrice = 15;
            var newSodaQuantity = 10;

            var newSoda = new Soda(newSodaName, newSodaPrice, newSodaQuantity);

            // Act
            var result = _storageService.AddInventory(newSoda);
            var storedSoda = _storageService.GetSoda(newSodaName);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(storedSoda.Name == newSodaName);
            Assert.IsTrue(storedSoda.Price == newSodaPrice);
            Assert.IsTrue(storedSoda.Quantity == newSodaQuantity);
        }

        [TestMethod]
        public void AddInventory_AddingExistingSoda_ReturnsFalse()
        {
            // Arrange
            var existingSodaName = "fanta";
            var sodaPrice = 15;
            var sodaQuantity = 10;

            var newSoda = new Soda(existingSodaName, sodaPrice, sodaQuantity);

            // Act
            var result = _storageService.AddInventory(newSoda);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetSoda_SendInExistingSodaName_ReturnsSoda()
        {
            // Arrange
            var existingSodaName = "fanta";

            // Act
            var result = _storageService.GetSoda(existingSodaName);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Name == existingSodaName);
        }

        [TestMethod]
        public void GetSoda_SendInNonExistingSodaName_ReturnsNull()
        {
            // Arrange
            var existingSodaName = "solo";

            // Act
            var result = _storageService.GetSoda(existingSodaName);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetInventory_ShouldReturnInventoryWithTwoSoda()
        {
            // Act
            var result = _storageService.GetInventory();

            // Assert
            Assert.IsTrue(result.Count == 2);
        }
    }
}
