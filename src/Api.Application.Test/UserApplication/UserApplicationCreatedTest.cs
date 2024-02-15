using Api.Application.Controllers;
using Api.Domain.Dto.User;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.UserApplication
{
    public class UserApplicationCreatedTest
    {
        private UserController _usersController;

        [Fact(DisplayName = "É Possivel Executando Metodo Insert")]
        public async Task E_Possivel_Executar_Metodo_Insert()
        {
            var userServiceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            userServiceMock.Setup(mk => mk.InsertAsync(It.IsAny<UserDtoCreate>())).ReturnsAsync(
                
                new UserDtoCreateResult() {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    DateCreate = DateTime.UtcNow            
            });

            _usersController = new UserController(userServiceMock.Object);

            var url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

            _usersController.Url = url.Object;

            var userDto = new UserDtoCreate()
            {
                Name = nome,
                Email = email
            };

            var result = await _usersController.Insert(userDto);
            var resultValues = ((CreatedResult)result).Value as UserDtoCreateResult;

            Assert.True(result is CreatedResult);
            Assert.NotNull(resultValues);
            Assert.True(_usersController.ModelState.IsValid);
            Assert.Equal(resultValues.Name, userDto.Name);
            Assert.Equal(resultValues.Email, userDto.Email);

        }

        [Fact(DisplayName = "Não É Possivel Executando Metodo Insert")]
        public async Task Nao_E_Possivel_Executar_Metodo_Insert()
        {
            var userServiceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            userServiceMock.Setup(mk => mk.InsertAsync(It.IsAny<UserDtoCreate>())).ReturnsAsync(

                new UserDtoCreateResult()
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    DateCreate = DateTime.UtcNow
                });

            _usersController = new UserController(userServiceMock.Object);
            _usersController.ModelState.AddModelError("Name", "É um campo obrigatorio");

            var url = new Mock<IUrlHelper>();
            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

            _usersController.Url = url.Object;

            var userDto = new UserDtoCreate()
            {
                Name = nome,
                Email = email
            };

            var result = await _usersController.Insert(userDto);
            //var resultValues = ((CreatedResult)result).Value as UserDtoCreateResult;

            Assert.True(result is BadRequestObjectResult);
            Assert.False(_usersController.ModelState.IsValid);

        }
    }
}
