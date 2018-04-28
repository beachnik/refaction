using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using refactor_me.Repositories;
using refactor_me.Models;

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

            Console.Write(test.Description);
        }
    }
}
