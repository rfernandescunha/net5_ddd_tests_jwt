using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test.UserIntegration
{
    public class UserIntegrationDeleteTest : BaseIntegration
    {

        [Fact]
        public async Task E_Possivel_Realizar_Delete()
        {
            await AdicionarToken();


            var response = await httpClient.DeleteAsync($"{hostApi}User/{ Guid.Parse("A6998B7D-37DB-4C76-8F5F-828A98A890C7")} ");


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<bool>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(retorno);
        }
    }
}
