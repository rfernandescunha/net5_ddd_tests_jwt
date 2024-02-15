using Api.Domain.Dto.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test.UserIntegration
{
    public class UserIntegrationInsertTest: BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }

        [Fact]
        public async Task E_Possivel_Realizar_Insert()
        {
            await AdicionarToken();

            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            var userDto = new UserDtoCreate()
            {
                Name = _name,
                Email = _email
            };


            var response = await PostJsonAsync(userDto, $"{hostApi}User", httpClient);
            var postResult = await response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<UserDtoCreateResult>(postResult);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_name, retorno.Name);
            Assert.Equal(_email, retorno.Email);
            Assert.False(retorno.Id == default(Guid));
        }
    }
}
