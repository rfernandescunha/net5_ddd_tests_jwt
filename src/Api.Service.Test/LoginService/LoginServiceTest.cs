using Api.Domain.Dto;
using domain.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.LoginService
{
    public class LoginServiceTest
    {
        private ILoginService _LoginService;
        private Mock<ILoginService> _LoginServiceMock;


        [Fact(DisplayName = "É Possivel Executando Metodo Find By Login")]
        public async Task E_Possivel_Executar_Metodo_Find_By_Login()
        {
            var email = Faker.Internet.Email();
            var objRetorno = new LoginResponseDtoResult
            {
                authenticated = true,
                createDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                expirationDate = DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = Guid.NewGuid().ToString(),
                email = email,
                name = Faker.Name.FullName(),
                message = "Usuário Logado com sucesso"
            };

            var loginDto = new LoginDto()
            {
                Email = email
            };

            _LoginServiceMock = new Mock<ILoginService>();
            _LoginServiceMock.Setup(mk => mk.FindByLogin(loginDto)).ReturnsAsync(objRetorno);

            _LoginService = _LoginServiceMock.Object;

            var result = await _LoginService.FindByLogin(loginDto);

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Não É Possivel Executando Metodo Find By Login")]
        public async Task Nao_E_Possivel_Executar_Metodo_Find_By_Login()
        {
            _LoginServiceMock = new Mock<ILoginService>();
            _LoginServiceMock.Setup(mk => mk.FindByLogin(It.IsAny<LoginDto>())).Returns(Task.FromResult((LoginResponseDtoResult)null));

            _LoginService = _LoginServiceMock.Object;

            var result = await _LoginService.FindByLogin(It.IsAny<LoginDto>());

            Assert.Null(result);
        }
    }
}
