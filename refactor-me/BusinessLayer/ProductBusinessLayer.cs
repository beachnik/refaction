using refactor_me.Interfaces;
using refactor_me.Models;
using System;
using System.Collections.Generic;
using Unity;

namespace refactor_me.BusinessLayer
{
    public class ProductBusinessLayer : IProductBusinessLayer
    {
        private IProductRepository productRepo;
        private IProductOptionRepository productOptionRepo;

        public ProductBusinessLayer()
        {
            productRepo = UnityConfig.Container.Resolve<IProductRepository>();
            productOptionRepo = UnityConfig.Container.Resolve<IProductOptionRepository>();
        }

        #region Products

        public List<Product> GetAllProducts()
        {
            return productRepo.GetAll();
        }

        public Product GetProductByID(Guid id)
        {
            return productRepo.GetByID(id);
        }

        public List<Product> GetProductByName(string name)
        {
            return productRepo.GetByName(name);
        }

        public bool SaveNewProduct(Product p)
        {
            if (GetProductByID(p.Id) == null)
            {
                productRepo.Insert(p);
                return true;
            }
            return false;
        }

        public bool UpdateProduct(Product p)
        {
            if (GetProductByID(p.Id) != null)
            {
                productRepo.Update(p);
                return true;
            }
            return false;
        }

        public bool DeleteProduct(Guid id)
        {
            Product toBeDeleted = GetProductByID(id);
            if (toBeDeleted != null)
            {
                productRepo.Delete(toBeDeleted);
                return true;
            }
            return false;
        }

        #endregion

        #region ProductOptions

        public List<ProductOption> GetOptionsForProduct(Guid id)
        {
            return productOptionRepo.GetByProductID(id);
        }

        public ProductOption GetOptionById(Guid id)
        {
            return productOptionRepo.GetByID(id);
        }

        public bool SaveNewProductOption(ProductOption option)
        {
            if (GetOptionById(option.Id) == null)
            {
                productOptionRepo.Insert(option);
                return true;
            }
            return false;
        }

        public bool UpdateProductOption(ProductOption option)
        {
            if (GetOptionById(option.Id) != null)
            {
                productOptionRepo.Update(option);
                return true;
            }
            return false;
        }

        public bool DeleteProductOption(Guid optionId)
        {
            var toBeDeleted = GetOptionById(optionId);
            if (toBeDeleted != null)
            {
                productOptionRepo.Delete(toBeDeleted);
                return true;
            }
            return false;
        }

        #endregion
    }
}