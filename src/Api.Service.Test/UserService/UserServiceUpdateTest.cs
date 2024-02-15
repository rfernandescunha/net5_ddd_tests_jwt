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
    public class UserServiceUpdateTest : BaseUserServiceTest
    {
        private IUserService _userService;
        private Mock<IUserService> _userServiceMock;

        [Fact(DisplayName = "É Possivel Executando Metodo Update")]
        public async Task E_Possivel_Executar_Metodo_Update()
        {
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(mk => mk.InsertAsync(userDtoCreate)).ReturnsAsync(userDtoCreateResult);

            _userService = _userServiceMock.Object;

            var resultInsert = await _userService.InsertAsync(userDtoCreate);

            Assert.NotNull(resultInsert);
            Assert.True(resultInsert.Id == IdUser);
            Assert.Equal(resultInsert.Name, NameUser);



            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(mk => mk.UpdateAsync(userDtoUpdate)).ReturnsAsync(userDtoUpdateResult);

            _userService = _userServiceMock.Object;

            var resultUpdate = await _userService.UpdateAsync(userDtoUpdate);

            Assert.NotNull(resultUpdate);
            Assert.True(resultUpdate.Id == IdUser);
            Assert.Equal(resultUpdate.Name, NameUserAterado);
            Assert.Equal(resultUpdate.Email, EmailUserAterado);
        }

        [Fact(DisplayName = "Não é Possivel Executando Metodo Update")]
        public async Task Nao_E_Possivel_Executar_Metodo_Update()
        {
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(mk => mk.InsertAsync(userDtoCreate)).ReturnsAsync(userDtoCreateResult);

            _userService = _userServiceMock.Object;

            var resultInsert = await _userService.InsertAsync(userDtoCreate);

            Assert.NotNull(resultInsert);
            Assert.True(resultInsert.Id == IdUser);
            Assert.Equal(resultInsert.Name, NameUser);


            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(mk => mk.UpdateAsync(It.IsAny<UserDtoUpdate>())).Returns(Task.FromResult((UserDtoUpdateResult)null));

            _userService = _userServiceMock.Object;

            var result = await _userService.UpdateAsync(userDtoUpdate);

            Assert.Null(result);
        }
    }
}