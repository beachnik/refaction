using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using refactor_me.Interfaces;
using refactor_me.Models;
using System.Data.SqlClient;

namespace refactor_me.Repositories
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        public void Delete(ProductOption p)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = new SqlCommand($"delete from productoption where id = '{p.Id}'", conn);
            cmd.ExecuteReader();
        }

        public List<ProductOption> GetAll()
        {
            List<ProductOption> Items = new List<ProductOption>();
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"select * from productoption", conn);
            conn.Open();

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Items.Add(CreateProductOptionFromReader(rdr));
            }
            return Items;
        }

        public ProductOption GetByID(Guid id)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"select * from productoption where id = '{id}'", conn);
            conn.Open();
            var rdr = cmd.ExecuteReader();
            if (!rdr.Read())
                //TODO: Throw some manner of exception here, or upstream
                return null;

            return CreateProductOptionFromReader(rdr);
        }

        public List<ProductOption> GetByProductID(Guid productId)
        {
            List<ProductOption> Items = new List<ProductOption>();
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"select * from productoption where productid = '{productId}'", conn);
            conn.Open();

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Items.Add(CreateProductOptionFromReader(rdr));
            }
            return Items;
        }

        public void Insert(ProductOption p)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"insert into productoption (id, productid, name, description) values ('{p.Id}', '{p.ProductId}', '{p.Name}', '{p.Description}')", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(ProductOption p)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"update productoption set name = '{p.Name}', description = '{p.Description}' where id = '{p.Id}'", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
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