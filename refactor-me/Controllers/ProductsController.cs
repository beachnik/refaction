using refactor_me.Interfaces;
using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Unity;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private IProductBusinessLayer busLayer;

        public ProductsController()
        {
            busLayer = UnityConfig.Container.Resolve<IProductBusinessLayer>();
        }

        #region Products
        [Route]
        [HttpGet]
        public List<Product> GetAll()
        {
            return busLayer.GetAllProducts();
        }

        [Route]
        [HttpGet]
        public List<Product> SearchByName(string name)
        {
            return busLayer.GetProductByName(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            var product = busLayer.GetProductByID(id);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return product;
        }

        [Route]
        [HttpPost]
        public void Create(Product product)
        {
            if (!busLayer.SaveNewProduct(product))
            {
                //If the GUID has already been used in the DB we return null 
                //and throw a conflict response header.  Not sure if this
                //is quite the correct response but I felt something was required
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, Product product)
        {
            //Something weird must have happened if the Id for the object and the
            //passed Id don't match.  Throw an error.
            if (product.Id != id)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //If we couldn't update the product it's almost certainly because
            //we couldn't find it.
            if (!busLayer.UpdateProduct(product))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            if (!busLayer.DeleteProduct(id))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        #endregion


        #region ProductOptions
        [Route("{productId}/options")]
        [HttpGet]
        public List<ProductOption> GetOptions(Guid productId)
        {
            return busLayer.GetOptionsForProduct(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var option = busLayer.GetOptionById(id);
            if (option == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            //Not sure how essential this is, but leaving it will do no harm.
            option.ProductId = productId;
            busLayer.SaveNewProductOption(option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            busLayer.UpdateProductOption(option);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            if (!busLayer.DeleteProductOption(id))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        #endregion
    }
}
