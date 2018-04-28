using refactor_me.Models;
using System;
using System.Collections.Generic;

namespace refactor_me.Interfaces
{
    public interface IProductOptionRepository : IRepository<ProductOption>
    {
        List<ProductOption> GetByProductID(Guid productId);
        
    }
}
