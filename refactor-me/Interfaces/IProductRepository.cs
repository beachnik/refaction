using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using refactor_me.Models;

namespace refactor_me.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetByName(string name);
    }
}
