using Api.Domain.Entities;
using Api.Domain.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserDtoCreateResult> InsertAsync(UserDtoCreate item);
        Task<UserDtoUpdateResult> UpdateAsync(UserDtoUpdate item);
        Task<bool> DeleteAsync(Guid Id);
        Task<UserDtoFindResult> FindAsync(Guid Id);
        Task<IEnumerable<UserDtoFindResult>> FindAsync(Expression<Func<UserDtoFind, bool>> predicate = null);
    }
}
