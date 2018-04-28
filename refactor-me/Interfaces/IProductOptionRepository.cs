using refactor_me.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Interfaces
{
    public interface IProductOptionRepository : IRepository<ProductOption>
    {
        List<ProductOption> GetByProductID(Guid productId);
        
    }
}
