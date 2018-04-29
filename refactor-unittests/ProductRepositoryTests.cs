using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Repositories;
using refactor_me.Models;
using System.Collections.Generic;

namespace refactor_unittests
{
    [TestClass]
    public class ProductRepositoryTests
    {
        [TestMethod]
        public void GetByIDTest()
        {
            ProductRepository repo = new ProductRepository();

            Product test = repo.GetByID(Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));

            Assert.AreEqual("Newest mobile product from Samsung.", test.Description);
            
        }

        [TestMethod]
        public void GetMissingEntryByIDTest()
        {
            ProductRepository repo = new ProductRepository();

            Product test = repo.GetByID(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            Assert.IsNull(test);
        }

        [TestMethod]
        public void GetByNameTest()
        {
            ProductRepository repo = new ProductRepository();

            List<Product> results = repo.GetByName("Samsung");

            Assert.IsTrue(results.Count == 1);

            Assert.AreEqual("Newest mobile product from Samsung.", results[0].Description);
        }
    }
}
