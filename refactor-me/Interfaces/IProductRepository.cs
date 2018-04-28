using refactor_me.Models;
using System.Collections.Generic;

namespace refactor_me.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetByName(string name);
    }
}
