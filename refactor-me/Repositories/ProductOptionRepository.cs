using refactor_me.Interfaces;
using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace refactor_me.Repositories
{
    public class ProductOptionRepository : SQLDataReaderRepository, IProductOptionRepository
    {
        public void Delete(ProductOption p)
        {
            ExecuteNonReaderQuery($"delete from productoption where id = '{p.Id}'");
        }

        public List<ProductOption> GetAll()
        {
            List<ProductOption> Items = new List<ProductOption>();
            var rdr = ExecuteReaderQuery("select * from productoption");
            while (rdr.Read())
            {
                Items.Add(CreateProductOptionFromReader(rdr));
            }
            return Items;
        }

        public ProductOption GetByID(Guid id)
        {
            var rdr = ExecuteReaderQuery($"select * from productoption where id = '{id}'");
            if (!rdr.Read())
                //TODO: Throw some manner of exception here, or upstream
                return null;

            return CreateProductOptionFromReader(rdr);
        }

        public List<ProductOption> GetByProductID(Guid productId)
        {
            List<ProductOption> Items = new List<ProductOption>();
            var rdr = ExecuteReaderQuery($"select * from productoption where productid = '{productId}'");
            while (rdr.Read())
            {
                Items.Add(CreateProductOptionFromReader(rdr));
            }
            return Items;
        }

        public void Insert(ProductOption p)
        {
            ExecuteNonReaderQuery($"insert into productoption (id, productid, name, description) values ('{p.Id}', '{p.ProductId}', '{p.Name}', '{p.Description}')");
        }

        public void Update(ProductOption p)
        {
            ExecuteNonReaderQuery($"update productoption set name = '{p.Name}', description = '{p.Description}' where id = '{p.Id}'");
        }

        private ProductOption CreateProductOptionFromReader(SqlDataReader rdr)
        {
            return new ProductOption(Guid.Parse(rdr["Id"].ToString()),
                Guid.Parse(rdr["ProductId"].ToString()),
                rdr["Name"].ToString(),
                (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString());
        }
    }
}