using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Interfaces;
using refactor_me.Models;
using System.Data.SqlClient;

namespace refactor_me.Repositories
{
    public class ProductRepository : SQLDataReaderRepository, IProductRepository
    {
        public void Delete(Product p)
        {
            //We presume at this point that something in the business layer has already
            //removed the options, and checked for the presence of the product to be
            //deleted.
            ExecuteNonReaderQuery($"delete from product where id = '{p.Id}'");
        }

        public List<Product> GetAll()
        {
            List<Product> allProducts = new List<Product>();
            var rdr = ExecuteReaderQuery("select * from product");
            while (rdr.Read())
            {
                allProducts.Add(CreateProductFromReader(rdr));
            }
            
            return allProducts;
        }

        public Product GetByID(Guid id)
        {
            var rdr = ExecuteReaderQuery($"select * from product where id = '{id}'");
            if (!rdr.Read())
                //TODO: Throw some manner of exception here, or upstream
                return null;

            return CreateProductFromReader(rdr);
        }

        public List<Product> GetByName(string name)
        {
            List<Product> products = new List<Product>();
            var rdr = ExecuteReaderQuery($"select id from product where lower(name) like '%{name.ToLower()}%'");
            while (rdr.Read())
            {
                products.Add(CreateProductFromReader(rdr));
            }
            return products;
        }
        
        public void Insert(Product p)
        {
            ExecuteNonReaderQuery($"insert into product (id, name, description, price, deliveryprice) values ('{p.Id}', '{p.Name}', '{p.Description}', {p.Price}, {p.DeliveryPrice})");
        }

        public void Update(Product p)
        {
            ExecuteNonReaderQuery($"update product set name = '{p.Name}', description = '{p.Description}', price = {p.Price}, deliveryprice = {p.DeliveryPrice} where id = '{p.Id}'");
        }

        private Product CreateProductFromReader(SqlDataReader rdr)
        {
            return new Product(Guid.Parse(rdr["Id"].ToString()),
                rdr["Name"].ToString(),
                (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                decimal.Parse(rdr["Price"].ToString()),
                decimal.Parse(rdr["DeliveryPrice"].ToString()));
        }
    }

}