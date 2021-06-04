using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        bool Add(T entity);
        bool RemoveFirst(Func<T, bool> predicate);
        int RemoveWhere(Func<T, bool> predicate);
        T GetFirst(Func<T, bool> predicate);
        IEnumerable<T> GetWhere(Func<T, bool> predicate);
        bool Contains(Func<T, bool> predicate);
    }
}
