using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Api.Domain.Security;
using Api.Domain.Dto;
using domain.Interfaces.Services;
using domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private IConfiguration _configuration;

        public SigningConfiguration _signingConfiguration;
        //public TokenConfiguration _tokenConfiguration;

        public LoginService(
                            IUserRepository repository,
                            IConfiguration configuration,
                            SigningConfiguration signingConfiguration
                            //TokenConfiguration tokenConfiguration
            )
        {
            _repository = repository;
            _configuration = configuration;
            _signingConfiguration = signingConfiguration;
            //_tokenConfiguration = tokenConfiguration;
        }
        public async Task<LoginResponseDtoResult> FindByLogin(LoginDto login)
        {
            var user = new User();

            if (!String.IsNullOrEmpty(login.Email))
            {
                user = await _repository.FindByLogin(login.Email);

                if(user == null)
                {
                    return new LoginResponseDtoResult
                    {
                        authenticated = false,
                        message = "Falha ao authenticar"
                    };
                }
                else
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(user.Email),
                        new[]
                                { 
                                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                    new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.UniqueName, user.Email)
                                }
                        );

                    var dateCreate = DateTime.Now;
                    var dateCreateExpiration = dateCreate + TimeSpan.FromSeconds(Convert.ToInt32(Environment.GetEnvironmentVariable("Seconds")));
                    //var dateCreateExpiration = dateCreate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                    
                    string token = CreateToken(identity, dateCreate, dateCreateExpiration, new JwtSecurityTokenHandler());

                    return SuccessObject(dateCreate, dateCreateExpiration, token, user);
                }
            }
            else
            {
                return new LoginResponseDtoResult
                {
                    authenticated = false,
                    message = "Falha ao authenticar"
                };
            }
            
        }

        private string CreateToken(ClaimsIdentity identity, DateTime dateCreate, DateTime dateCreateExpiration, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                //Issuer = _tokenConfiguration.Issuer,
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                //Audience = _tokenConfiguration.Audience,
                Audience = Environment.GetEnvironmentVariable("Audience"),
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = dateCreate,
                Expires = dateCreateExpiration,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private LoginResponseDtoResult SuccessObject(DateTime createDate, DateTime expirationDate, string token, User user)
        {
            return new LoginResponseDtoResult
            {
                authenticated = true,
                createDate = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expirationDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                email = user.Email,
                name = user.Name,
                message = "Usuário Logado com sucesso"
            };
        }
    }
}
