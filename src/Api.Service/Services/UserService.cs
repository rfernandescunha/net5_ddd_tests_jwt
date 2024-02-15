using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using AutoMapper;
using Api.Domain.Dto.User;
using Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(
                            IUserRepository repository,
                            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<UserDtoCreateResult> InsertAsync(UserDtoCreate userDto)
        {
            try
            {
                var model = _mapper.Map<UserModel>(userDto);
                var entity = _mapper.Map<User>(model);

                var retorno = await _repository.InsertAsync(entity);

                return _mapper.Map<UserDtoCreateResult>(retorno);
            }
            catch (System.Exception)
            {                
                throw;
            }
        }

        public async Task<UserDtoUpdateResult> UpdateAsync(UserDtoUpdate userDto)
        {
            try
            {
                var model = _mapper.Map<UserModel>(userDto);
                var entity = _mapper.Map<User>(model);

                var retorno = await _repository.UpdateAsync(entity);

                return _mapper.Map<UserDtoUpdateResult>(retorno);
            }
            catch (System.Exception)
            {                
                throw;
            }
        }
        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var item = await FindAsync(Id);

                if(item == null)
                    return false;
                
                return await _repository.DeleteAsync(Id);
            }
            catch (System.Exception)
            {                
                throw;
            }
        }

        public async Task<UserDtoFindResult> FindAsync(Guid Id)
        {
            try
            {
                var retorno = await _repository.FindAsync(Id);

                return _mapper.Map<UserDtoFindResult>(retorno) ?? new UserDtoFindResult();
            }
            catch (System.Exception)
            {                
                throw;
            }
        }

        public async Task<IEnumerable<UserDtoFindResult>> FindAsync(Expression<Func<UserDtoFind, bool>> predicate = null)
        {
            try
            {
                var model = _mapper.Map<Expression<Func<UserModel, bool>> >(predicate);
                var entity = _mapper.Map<Expression<Func<User, bool>>>(model);

                var retorno =  await _repository.FindAsync(entity);

                return _mapper.Map<IEnumerable<UserDtoFindResult>>(retorno);
            }
            catch (System.Exception)
            {                
                throw;
            }
        }


    }
}
