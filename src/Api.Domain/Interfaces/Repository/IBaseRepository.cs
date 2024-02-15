using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
         Task<T> InsertAsync(T item);
         Task<T> UpdateAsync(T item);
         Task<bool> DeleteAsync(Guid Id);
         Task<T> FindAsync(Guid Id);
         Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate = null);
         Task<bool> ExistAsync(Guid Id);
    }
}
