using Api.Application.Controllers;
using Api.Domain.Dto.User;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.UserApplication
{
    public class UserApplicationFindTest
    {
        private UserController _usersController;

        [Fact(DisplayName = "É Possivel Executando Metodo Find By")]
        public async Task E_Possivel_Executar_Metodo_Find_By()
        {
            var userServiceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            userServiceMock.Setup(mk => mk.FindAsync(It.IsAny<Guid>())).ReturnsAsync(

                new UserDtoFindResult()
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    DateCreate = DateTime.UtcNow,
                    DateUpdate = DateTime.UtcNow
                });

            _usersController = new UserController(userServiceMock.Object);

            var result = await _usersController.Find(Guid.NewGuid());
            var resultValues = ((OkObjectResult)result).Value as UserDtoFindResult;

            Assert.True(result is OkObjectResult);
            Assert.NotNull(resultValues);
            Assert.Equal(resultValues.Name, nome);
            Assert.Equal(resultValues.Email, email);

        }

        [Fact(DisplayName = "Não É Possivel Executando Metodo Find BY")]
        public async Task Nao_E_Possivel_Executar_Metodo_Find_By()
        {
            var userServiceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            userServiceMock.Setup(mk => mk.FindAsync(It.IsAny<Guid>())).ReturnsAsync(

                new UserDtoFindResult()
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    DateCreate = DateTime.UtcNow,
                    DateUpdate = DateTime.UtcNow
                });

            _usersController = new UserController(userServiceMock.Object);
            _usersController.ModelState.AddModelError("Id", "É um campo obrigatorio");

            var result = await _usersController.Find(default(Guid));
            //var resultValues = ((OkObjectResult)result).Value as UserDtoFindResult;

            Assert.True(result is BadRequestObjectResult);
            Assert.False(_usersController.ModelState.IsValid);

        }

        [Fact(DisplayName = "É Possivel Executando Metodo Find Expression")]
        public async Task E_Possivel_Executar_Metodo_Find_Expression()
        {
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(mk => mk.FindAsync(It.IsAny<Expression<Func<UserDtoFind, bool>>>())).ReturnsAsync(

                new List<UserDtoFindResult>()
                {
                    new UserDtoFindResult()
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        DateCreate = DateTime.UtcNow,
                        DateUpdate = DateTime.UtcNow
                    },
                    new UserDtoFindResult()
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        DateCreate = DateTime.UtcNow,
                        DateUpdate = DateTime.UtcNow
                    },
                    new UserDtoFindResult()
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        DateCreate = DateTime.UtcNow,
                        DateUpdate = DateTime.UtcNow
                    }
                }

            );

            _usersController = new UserController(userServiceMock.Object);

            var result = await _usersController.Find(new UserDtoFind());
            var resultValues = ((OkObjectResult)result).Value as IEnumerable<UserDtoFindResult>;

            Assert.True(result is OkObjectResult);
            Assert.NotNull(resultValues);
            Assert.True(resultValues.Count() == 3);

        }

        [Fact(DisplayName = "Não É Possivel Executando Metodo Find Expression")]
        public async Task Nao_E_Possivel_Executar_Metodo_Find_Expression()
        {
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(mk => mk.FindAsync(It.IsAny<Expression<Func<UserDtoFind, bool>>>())).ReturnsAsync(

                new List<UserDtoFindResult>()
                {
                    new UserDtoFindResult()
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        DateCreate = DateTime.UtcNow,
                        DateUpdate = DateTime.UtcNow
                    },
                    new UserDtoFindResult()
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        DateCreate = DateTime.UtcNow,
                        DateUpdate = DateTime.UtcNow
                    },
                    new UserDtoFindResult()
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        DateCreate = DateTime.UtcNow,
                        DateUpdate = DateTime.UtcNow
                    }
                }

            );

            _usersController = new UserController(userServiceMock.Object);
            _usersController.ModelState.AddModelError("Id", "Formato Invalido");

            var result = await _usersController.Find(new UserDtoFind());
            //var resultValues = ((OkObjectResult)result).Value as IEnumerable<UserDtoFindResult>;

            Assert.True(result is BadRequestObjectResult);
            //Assert.NotNull(resultValues);
            //Assert.True(resultValues.Count() == 3);

        }
    }
}
