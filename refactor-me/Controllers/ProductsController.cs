using System;
using System.Net;
using System.Web.Http;
using refactor_me.Models;
using refactor_me.BusinessLayer;
using System.Collections.Generic;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private ProductBusinessLayer busLayer;

        public ProductsController()
        {
            busLayer = new ProductBusinessLayer();
        }

        #region Products
        [Route]
        [HttpGet]
        public List<Product> GetAll()
        {
            return busLayer.GetAll();
        }

        [Route]
        [HttpGet]
        public List<Product> SearchByName(string name)
        {
            return busLayer.GetByName(name);
        }

        [Route("{id}")]
        [HttpGet]
        public Product GetProduct(Guid id)
        {
            var product = busLayer.GetByID(id);
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
            busLayer.DeleteProduct(id);
        }

        #endregion


        #region ProductOptions
        [Route("{productId}/options")]
        [HttpGet]
        public ProductOptions GetOptions(Guid productId)
        {
            return new ProductOptions(productId);
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public ProductOption GetOption(Guid productId, Guid id)
        {
            var option = new ProductOption(id);
            if (option.IsNew)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return option;
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOption option)
        {
            option.ProductId = productId;
            option.Save();
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOption option)
        {
            var orig = new ProductOption(id)
            {
                Name = option.Name,
                Description = option.Description
            };

            if (!orig.IsNew)
                orig.Save();
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            var opt = new ProductOption(id);
            opt.Delete();
        }
        #endregion
    }
}
