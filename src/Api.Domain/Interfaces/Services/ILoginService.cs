using Api.Domain.Entities;
using Api.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task<LoginResponseDtoResult> FindByLogin(LoginDto login);
    }
}
