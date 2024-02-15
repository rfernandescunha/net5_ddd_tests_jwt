using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface IBaseServices<T> where T : class
    {
         Task<T> InsertAsync(T item);
         Task<T> UpdateAsync(T item);
         Task<bool> DeleteAsync(Guid Id);
         Task<T> FindAsync(Guid Id);
         Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate = null);
    }
}
