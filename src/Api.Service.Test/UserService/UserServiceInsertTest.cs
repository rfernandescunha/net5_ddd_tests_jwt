using Api.Domain.Interfaces.Services;
using Api.Domain.Dto.User;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.UserService
{
    public class UserServiceInsertTest: BaseUserServiceTest
    {
        private IUserService _userService;
        private Mock<IUserService> _userServiceMock;

        [Fact(DisplayName = "É Possivel Executando Metodo Insert")]
        public async Task E_Possivel_Executar_Metodo_Insert()
        {
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(mk => mk.InsertAsync(userDtoCreate)).ReturnsAsync(userDtoCreateResult);

            _userService = _userServiceMock.Object;

            var result = await _userService.InsertAsync(userDtoCreate);

            Assert.NotNull(result);
            Assert.True(result.Id == IdUser);
            Assert.Equal(result.Name, NameUser);
        }

        [Fact(DisplayName = "Não é Possivel Executando Metodo Insert")]
        public async Task Nao_E_Possivel_Executar_Metodo_Insert()
        {
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(mk => mk.InsertAsync(It.IsAny<UserDtoCreate>())).Returns(Task.FromResult((UserDtoCreateResult)null));

            _userService = _userServiceMock.Object;

            var result = await _userService.InsertAsync(userDtoCreate);

            Assert.Null(result);
        }
    }
}
