using Api.Application.Controllers;
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
    public class UserApplicationDeleteTest
    {
        private UserController _usersController;

        [Fact(DisplayName = "É Possivel Executando Metodo Delete")]
        public async Task E_Possivel_Executar_Metodo_Delete()
        {
            var userServiceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            userServiceMock.Setup(mk => mk.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            _usersController = new UserController(userServiceMock.Object);


            var result = await _usersController.Delete(Guid.NewGuid());

            var resultValues = ((OkObjectResult)result).Value;

            Assert.True(result is OkObjectResult);
            Assert.True(_usersController.ModelState.IsValid);
            Assert.NotNull(resultValues);
            Assert.True((bool)resultValues);

        }


        [Fact(DisplayName = "Não É Possivel Executando Metodo Delete")]
        public async Task Nao_E_Possivel_Executar_Metodo_Delete()
        {
            var userServiceMock = new Mock<IUserService>();

            userServiceMock.Setup(mk => mk.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            _usersController = new UserController(userServiceMock.Object);
            _usersController.ModelState.AddModelError("Id", "É um campo obrigatorio");

            var result = await _usersController.Delete(default(Guid));


            Assert.True(result is BadRequestObjectResult);
            Assert.False(_usersController.ModelState.IsValid);

        }
    }
}
