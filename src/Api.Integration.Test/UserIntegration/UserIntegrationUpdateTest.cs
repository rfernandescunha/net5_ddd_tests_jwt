using Api.Domain.Dto.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test.UserIntegration
{
    public class UserIntegrationUpdateTest : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }

        [Fact]
        public async Task E_Possivel_Realizar_Update()
        {
            await AdicionarToken();

            var userDtoUpdate = new UserDtoUpdate()
            {
                Id = Guid.Parse("B0539399-DD43-4C3D-B851-70AA3E67FE69"),
                Name = "Rafael Fernandes da Cunha",
                Email = "rafael.cunha@teste.com.br"                
            };


            var stringContent = new StringContent(JsonConvert.SerializeObject(userDtoUpdate), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"{hostApi}User", stringContent);


            var postResult = await response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<UserDtoUpdateResult>(postResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(userDtoUpdate.Name, retorno.Name);
            Assert.Equal(userDtoUpdate.Email, retorno.Email);
        }
    }
}
