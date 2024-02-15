using Api.Domain.Dto.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace Api.Integration.Test.UserIntegration
{
    public class UserIntegrationFindTest : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }

        [Fact]
        public async Task E_Possivel_Realizar_Find_Expression()
        {
            await AdicionarToken();

            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            var userDto = new UserDtoFind()
            {
                Name = _name,
                Email = _email
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{hostApi}User"),
                Content = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(request);


            //string qString = String.Concat($"{hostApi}User/?Name=", HttpUtility.UrlEncode(_name), "&Email=", HttpUtility.UrlEncode(_name));
            //var response = await httpClient.GetAsync(qString);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listaRetorno = JsonConvert.DeserializeObject<IEnumerable<UserDtoFindResult>>(jsonResult);

            Assert.NotNull(listaRetorno);
            Assert.True(listaRetorno.Count() > 0);
            Assert.True(listaRetorno.Where(x => x.Id == Guid.Parse("B0539399-DD43-4C3D-B851-70AA3E67FE69")).Count() == 1);
;
        }

        [Fact]
        public async Task E_Possivel_Realizar_Find_Id()
        {
            await AdicionarToken();

 
            var response = await httpClient.GetAsync($"{hostApi}User/{ Guid.Parse("B0539399-DD43-4C3D-B851-70AA3E67FE69")} ");


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<UserDtoFindResult>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(retorno);
            Assert.True(retorno.Id == Guid.Parse("B0539399-DD43-4C3D-B851-70AA3E67FE69"));
        }
    }
}
