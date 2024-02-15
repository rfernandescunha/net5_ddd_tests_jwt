using Api.Domain.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.UserService
{
    public class UserServiceDeleteTest : BaseUserServiceTest
    {
        private IUserService _userService;
        private Mock<IUserService> _userServiceMock;

        [Fact(DisplayName = "É Possivel Executando Metodo Delete")]
        public async Task E_Possivel_Executar_Metodo_Delete()
        {
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(x => x.DeleteAsync(IdUser)).ReturnsAsync(true);

            _userService = _userServiceMock.Object;

            var result = await _userService.DeleteAsync(IdUser);

            Assert.True(result);
        }

        [Fact(DisplayName = "Não é Possivel Executando Metodo Delete")]
        public async Task Nao_E_Possivel_Executar_Metodo_Delete()
        {
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(x => x.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            _userService = _userServiceMock.Object;

            var result = await _userService.DeleteAsync(IdUser);

            Assert.False(result);
        }
    }
}

