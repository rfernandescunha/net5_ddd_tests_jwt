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
    public class UserApplicationUpdateTest
    {
        private UserController _usersController;

        [Fact(DisplayName = "É Possivel Executando Metodo Update")]
        public async Task E_Possivel_Executar_Metodo_Update()
        {
            var userServiceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            userServiceMock.Setup(mk => mk.UpdateAsync(It.IsAny<UserDtoUpdate>())).ReturnsAsync(

                new UserDtoUpdateResult()
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    DateCreate = DateTime.UtcNow,
                    DateUpdate = DateTime.UtcNow
                });

            _usersController = new UserController(userServiceMock.Object);

            var userDto = new UserDtoUpdate()
            {
                Id = Guid.NewGuid(),
                Name = nome,
                Email = email
            };

            var result = await _usersController.Update(userDto);
            var resultValues = ((OkObjectResult)result).Value as UserDtoUpdateResult;

            Assert.True(result is OkObjectResult);
            Assert.NotNull(resultValues);
            Assert.True(_usersController.ModelState.IsValid);
            Assert.Equal(resultValues.Name, userDto.Name);
            Assert.Equal(resultValues.Email, userDto.Email);

        }

        [Fact(DisplayName = "Não É Possivel Executando Metodo Update")]
        public async Task Nao_E_Possivel_Executar_Metodo_Update()
        {
            var userServiceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            userServiceMock.Setup(mk => mk.UpdateAsync(It.IsAny<UserDtoUpdate>())).ReturnsAsync(

                new UserDtoUpdateResult()
                {
                    Id = Guid.NewGuid(),
                    Name = nome,
                    Email = email,
                    DateCreate = DateTime.UtcNow
                });

            _usersController = new UserController(userServiceMock.Object);
            _usersController.ModelState.AddModelError("Email", "É um campo obrigatorio");

            var userDto = new UserDtoUpdate()
            {
                Id = Guid.NewGuid(),
                Name = nome,
                Email = email
            };

            var result = await _usersController.Update(userDto);
            //var resultValues = ((CreatedResult)result).Value as UserDtoUpdateResult;

            Assert.True(result is BadRequestObjectResult);
            Assert.False(_usersController.ModelState.IsValid);

        }
    }
}
