using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
             
        public Product(Guid id, string name, string description, decimal price, decimal deliveryprice)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.DeliveryPrice = deliveryprice;
        }
    }
}