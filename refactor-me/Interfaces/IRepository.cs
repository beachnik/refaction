using System;
using System.Collections.Generic;

namespace refactor_me.Interfaces
{
    public interface IRepository<T>
    {
        T GetByID(Guid id);
        
        List<T> GetAll();

        void Update(T p);

        void Insert(T p);

        void Delete(T p);
    }
}
