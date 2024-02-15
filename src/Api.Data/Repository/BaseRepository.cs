using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Api.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApiContext _context;
        private DbSet<T> _dataset;

        public BaseRepository(ApiContext context)
        {
            _context = context;
            _dataset = context.Set<T>();
        }
        public async Task<T> InsertAsync(T item)
        {
            if(item.Id == Guid.Empty)
                item.Id = Guid.NewGuid();

            item.DateCreate = DateTime.Now;

            _dataset.Add(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            item.DateUpdate = DateTime.Now;

            _dataset.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }
        public async Task<bool> DeleteAsync(Guid Id)
        {
            var item = await _dataset.SingleOrDefaultAsync(x=> x.Id.Equals(Id));

            _dataset.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<T> FindAsync(Guid Id)
        {
            return await _dataset.SingleOrDefaultAsync(x=> x.Id.Equals(Id));
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate = null)
        {
            _context.ChangeTracker.LazyLoadingEnabled = false;

            if (predicate != null)
            {
                return await _dataset.Where(predicate).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _dataset.ToListAsync();
            }
        }

        public async Task<bool> ExistAsync(Guid Id){
            return await _dataset.AnyAsync(x=> x.Id.Equals(Id));
        }
    }
}
