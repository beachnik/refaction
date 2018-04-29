using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.BusinessLayer;
using refactor_me.Models;
using System.Collections.Generic;

namespace refactor_unittests
{
    [TestClass]
    public class ProductBusinessLayerTests
    {
        [TestMethod]
        public void GetAllProductsTest()
        {
            ProductBusinessLayer layer = new ProductBusinessLayer();

            List<Product> allProducts = layer.GetAllProducts();

            Assert.AreEqual(2, allProducts.Count);
        }

        [TestMethod]
        public void GetByProductIdTest()
        {
            ProductBusinessLayer layer = new ProductBusinessLayer();

            Product test = layer.GetProductByID(Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));

            Assert.AreEqual("Newest mobile product from Samsung.", test.Description);
        }

        [TestMethod]
        public void GetProductByNameTest()
        {
            ProductBusinessLayer layer = new ProductBusinessLayer();

            List<Product> results = layer.GetProductByName("Samsung");

            Assert.IsTrue(results.Count == 1);

            Assert.AreEqual("Newest mobile product from Samsung.", results[0].Description);
        }

        [TestMethod]
        public void UpdateProductTest()
        {
            ProductBusinessLayer layer = new ProductBusinessLayer();

            Product test = layer.GetProductByID(Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));

            string description = test.Description;

            test.Description = "This is changed for a unit test";

            layer.UpdateProduct(test);

            Product postUpdate = layer.GetProductByID(Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));

            Assert.AreEqual(test.Description, postUpdate.Description);

            postUpdate.Description = description;

            layer.UpdateProduct(postUpdate);
        }

        [TestMethod]
        public void SaveAndDeleteProductTest()
        {
            ProductBusinessLayer layer = new ProductBusinessLayer();

            Guid id = new Guid();
            Product Test = new Product(id, "TestProduct", "This is a test product for unit testing purposes", 1, 2);

            layer.SaveNewProduct(Test);

            Product postInsert = layer.GetProductByID(id);

            Assert.AreEqual(Test.Id, postInsert.Id);
            Assert.AreEqual(Test.Name, postInsert.Name);
            Assert.AreEqual(Test.Description, postInsert.Description);
            Assert.AreEqual(Test.Price, postInsert.Price);
            Assert.AreEqual(Test.DeliveryPrice, postInsert.DeliveryPrice);

            layer.DeleteProduct(postInsert.Id);

            Product postDelete = layer.GetProductByID(id);

            Assert.IsNull(postDelete);
        }

        [TestMethod]
        public void GetOptionsForProductTest()
        {
            ProductBusinessLayer layer = new ProductBusinessLayer();

            List<ProductOption> options = layer.GetOptionsForProduct(Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"));

            Assert.AreEqual(2, options.Count);
        }

        [TestMethod]
        public void GetOptionByIdTest()
        {
            ProductBusinessLayer layer = new ProductBusinessLayer();

            ProductOption test = layer.GetOptionById(Guid.Parse("0643ccf0-ab00-4862-b3c5-40e2731abcc9"));

            Assert.AreEqual("White", test.Name);
            Assert.AreEqual("White Samsung Galaxy S7", test.Description);
            Assert.AreEqual(Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"), test.ProductId);
        }

        [TestMethod]
        public void UpdateProductOptionTest()
        {
            ProductBusinessLayer layer = new ProductBusinessLayer();

            ProductOption test = layer.GetOptionById(Guid.Parse("0643ccf0-ab00-4862-b3c5-40e2731abcc9"));

            string description = test.Description;

            test.Description = "This has been changed by a unit test";

            layer.UpdateProductOption(test);

            ProductOption postUpdate = layer.GetOptionById(Guid.Parse("0643ccf0-ab00-4862-b3c5-40e2731abcc9"));

            Assert.AreEqual(test.Description, postUpdate.Description);

            postUpdate.Description = description;

            layer.UpdateProductOption(postUpdate);
        }

        [TestMethod]
        public void SaveAndDeleteProductOptionTest()
        {
            ProductBusinessLayer layer = new ProductBusinessLayer();

            Guid id = new Guid();

            ProductOption test = new ProductOption(id, Guid.Parse("8f2e9176-35ee-4f0a-ae55-83023d2db1a3"), "TestProductOption", "This is a unit test created product option");

            layer.SaveNewProductOption(test);

            ProductOption postInsert = layer.GetOptionById(id);

            Assert.AreEqual(test.Id, postInsert.Id);
            Assert.AreEqual(test.ProductId, postInsert.ProductId);
            Assert.AreEqual(test.Name, postInsert.Name);
            Assert.AreEqual(test.Description, postInsert.Description);

            layer.DeleteProductOption(postInsert.Id);

            ProductOption postDelete = layer.GetOptionById(id);

            Assert.IsNull(postDelete);
        }

        [TestMethod]
        public void DeletingProductAlsoDeletesOptionsTest()
        {
            ProductBusinessLayer layer = new ProductBusinessLayer();

            Guid testProductId = new Guid();
            Guid testProductOptionId = new Guid();

            Product testProduct = new Product(testProductId, "TestProduct", "This Product is made by a unit test", 1, 2);

            layer.SaveNewProduct(testProduct);

            ProductOption testProductOption = new ProductOption(testProductOptionId, testProductId, "TestProductOption", "This option is made by a unit test");

            layer.SaveNewProductOption(testProductOption);

            //Check that they were saved successfully
            Product postInsertProduct = layer.GetProductByID(testProductId);
            ProductOption postInsertOption = layer.GetOptionById(testProductOptionId);

            Assert.IsNotNull(postInsertOption);
            Assert.IsNotNull(postInsertProduct);

            Assert.AreEqual(postInsertProduct.Id, postInsertOption.ProductId);

            layer.DeleteProduct(testProductId);

            Product postDeleteProduct = layer.GetProductByID(testProductId);
            ProductOption postDeleteOption = layer.GetOptionById(testProductOptionId);

            Assert.IsNull(postDeleteOption);
            Assert.IsNull(postDeleteProduct);

        }
    }
}
