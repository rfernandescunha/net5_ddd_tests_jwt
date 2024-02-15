using System.Threading.Tasks;
using Api.Domain.Entities;
using Api.Domain.Interfaces;

namespace Api.Domain.Interfaces.Repository
{
    public interface IUserRepository: IBaseRepository<User>
    {
         Task<User>FindByLogin(string email);
    }
}
