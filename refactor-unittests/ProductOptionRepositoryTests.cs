using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Repositories;
using refactor_me.Models;
using System.Collections.Generic;

namespace refactor_unittests
{
    [TestClass]
    public class ProductOptionRepositoryTests
    {
        [TestMethod]
        public void GetByIdTest()
        {
            ProductOptionRepository repo = new ProductOptionRepository();

            ProductOption test = repo.GetByID(Guid.Parse("0643ccf0-ab00-4862-b3c5-40e2731abcc9"));

            Assert.AreEqual("White", test.Name);
            Assert.AreEqual("White Samsung Galaxy S7", test.Description);
            Assert.AreEqual(Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"), test.ProductId);
        }

        [TestMethod]
        public void GetByIdFailureTest()
        {
            ProductOptionRepository repo = new ProductOptionRepository();

            ProductOption test = repo.GetByID(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            Assert.IsNull(test);
        }

        [TestMethod]
        public void GetByProductIdTest()
        {
            ProductOptionRepository repo = new ProductOptionRepository();

            List<ProductOption> options = repo.GetByProductID(Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));

            Assert.AreEqual(2, options.Count);
        }

        [TestMethod]
        public void UpdateTest()
        {
            ProductOptionRepository repo = new ProductOptionRepository();

            ProductOption test = repo.GetByID(Guid.Parse("0643ccf0-ab00-4862-b3c5-40e2731abcc9"));

            string description = test.Description;

            test.Description = "This has been changed by a unit test";

            repo.Update(test);

            ProductOption postUpdate = repo.GetByID(Guid.Parse("0643ccf0-ab00-4862-b3c5-40e2731abcc9"));

            Assert.AreEqual(test.Description, postUpdate.Description);

            postUpdate.Description = description;

            repo.Update(postUpdate);
        }

        [TestMethod]
        public void InsertAndDeleteTest()
        {
            ProductOptionRepository repo = new ProductOptionRepository();

            Guid id = new Guid();

            ProductOption test = new ProductOption(id, Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"), "TestProductOption", "This is a unit test created product option");

            repo.Insert(test);

            ProductOption postInsert = repo.GetByID(id);

            Assert.AreEqual(test.Id, postInsert.Id);
            Assert.AreEqual(test.ProductId, postInsert.ProductId);
            Assert.AreEqual(test.Name, postInsert.Name);
            Assert.AreEqual(test.Description, postInsert.Description);

            repo.Delete(postInsert);

            ProductOption postDelete = repo.GetByID(id);

            Assert.IsNull(postDelete);
        }
    }
}
