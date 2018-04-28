using refactor_me.Models;
using System;
using System.Collections.Generic;

namespace refactor_me.Interfaces
{
    public interface IProductBusinessLayer
    {
        List<Product> GetAllProducts();

        Product GetProductByID(Guid id);

        List<Product> GetProductByName(string name);

        bool SaveNewProduct(Product p);

        bool UpdateProduct(Product p);

        bool DeleteProduct(Guid id);

        List<ProductOption> GetOptionsForProduct(Guid id);

        ProductOption GetOptionById(Guid id);

        bool SaveNewProductOption(ProductOption option);

        bool UpdateProductOption(ProductOption option);

        bool DeleteProductOption(Guid optionId);
    }
}
