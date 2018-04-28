using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public ProductOption(Guid id, Guid productId, string name, string description)
        {
            this.Id = id;
            this.ProductId = productId;
            this.Name = name;
            this.Description = description;
        }
        
    }
}