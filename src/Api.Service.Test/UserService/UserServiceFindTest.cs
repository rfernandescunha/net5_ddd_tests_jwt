using Api.Domain.Interfaces.Repository;
using Api.Domain.Interfaces.Services;
using Api.Domain.Dto.User;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.UserService
{
    public class UserServiceFindTest: BaseUserServiceTest
    {
        private IUserService _userService;
        private Mock<IUserService> _userServiceMock;


        [Fact(DisplayName = "É Possivel Executando Metodo Find By Id")]
        public async Task E_Possivel_Executar_Metodo_Find_By_Id()
        {
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(mk => mk.FindAsync(IdUser)).ReturnsAsync(userDtoFindResult);

            _userService = _userServiceMock.Object;

            var result = await _userService.FindAsync(IdUser);

            Assert.NotNull(result);
            Assert.True(result.Id == IdUser);
            Assert.Equal(result.Name, NameUser);
        }

        [Fact(DisplayName = "Não É Possivel Executando Metodo Find By Id")]
        public async Task Nao_E_Possivel_Executar_Metodo_Find_By_Id()
        {
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(mk => mk.FindAsync(It.IsAny<Guid>())).Returns(Task.FromResult((UserDtoFindResult)null));

            _userService = _userServiceMock.Object;

            var result = await _userService.FindAsync(IdUser);

            Assert.Null(result);

        }



        [Fact(DisplayName = "É Possivel Executando Metodo Find By Expression")]
        public async Task E_Possivel_Executar_Metodo_Find_By_Expression()
        {
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(mk => mk.FindAsync(It.IsAny<Expression<Func<UserDtoFind, bool>>>())).ReturnsAsync(listUserDtoFindResult);

            _userService = _userServiceMock.Object;

            var result = await _userService.FindAsync();

            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }

        [Fact(DisplayName = "Não É Possivel Executando Metodo Find By Expression")]
        public async Task Nao_E_Possivel_Executar_Metodo_Find_By_Expression()
        {
            _userServiceMock = new Mock<IUserService>();
            _userServiceMock.Setup(mk => mk.FindAsync(It.IsAny<Expression<Func<UserDtoFind, bool>>>())).Returns(Task.FromResult((new List<UserDtoFindResult>().AsEnumerable())));

            _userService = _userServiceMock.Object;

            var result = await _userService.FindAsync();

            Assert.Empty(result);
            Assert.True(result.Count() == 0);

        }
    }
}
