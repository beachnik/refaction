using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Repositories;
using refactor_me.Interfaces;
using refactor_me.Models;

namespace refactor_me.BusinessLayer
{
    public class ProductBusinessLayer
    {
        private IProductRepository productRepo;
        private IProductOptionRepository productOptionRepo;

        public ProductBusinessLayer()
        {
            productRepo = new ProductRepository();
            productOptionRepo = new ProductOptionRepository();
        }

        public List<Product> GetAll()
        {
            return productRepo.GetAll();
        }

        public Product GetByID(Guid id)
        {
            return productRepo.GetByID(id);
        }

        public List<Product> GetByName(string name)
        {
            return productRepo.GetByName(name);
        }

        public bool SaveNewProduct(Product p)
        {
            if (GetByID(p.Id) == null)
            {
                productRepo.Insert(p);
                return true;
            }
            return false;
        }

        public bool UpdateProduct(Product p)
        {
            if (GetByID(p.Id) != null)
            {
                productRepo.Update(p);
                return true;
            }
            return false;
        }

        public bool DeleteProduct(Guid id)
        {
            Product toBeDeleted = GetByID(id);
            if (toBeDeleted != null)
            {
                productRepo.Delete(toBeDeleted);
                return true;
            }
            return false;
        }
    }
}