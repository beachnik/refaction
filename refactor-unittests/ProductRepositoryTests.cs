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

        [TestMethod]
        public void GetByNameFailureTest()
        {
            ProductRepository repo = new ProductRepository();

            List<Product> results = repo.GetByName("Banana");

            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void GetAllTest()
        {
            ProductRepository repo = new ProductRepository();

            List<Product> results = repo.GetAll();

            Assert.IsTrue(results.Count == 2);
        }

        [TestMethod]
        public void UpdateTest()
        {
            ProductRepository repo = new ProductRepository();

            Product test = repo.GetByID(Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));

            string description = test.Description;

            test.Description = "This is changed for a unit test";

            repo.Update(test);

            Product postUpdate = repo.GetByID(Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));

            Assert.AreEqual(test.Description, postUpdate.Description);

            postUpdate.Description = description;

            repo.Update(postUpdate);
        }

        [TestMethod]
        public void AddAndDeleteTest()
        {
            ProductRepository repo = new ProductRepository();
            Guid id = new Guid();
            Product Test = new Product(id, "TestProduct", "This is a test product for unit testing purposes", 1, 2);

            repo.Insert(Test);

            Product postInsert = repo.GetByID(id);

            Assert.AreEqual(Test.Id, postInsert.Id);
            Assert.AreEqual(Test.Name, postInsert.Name);
            Assert.AreEqual(Test.Description, postInsert.Description);
            Assert.AreEqual(Test.Price, postInsert.Price);
            Assert.AreEqual(Test.DeliveryPrice, postInsert.DeliveryPrice);

            repo.Delete(postInsert);

            Product postDelete = repo.GetByID(id);

            Assert.IsNull(postDelete);
        }
    }
}
