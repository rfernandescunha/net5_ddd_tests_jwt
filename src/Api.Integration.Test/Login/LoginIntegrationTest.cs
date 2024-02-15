using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test.Login
{
    public class LoginIntegrationTest: BaseIntegration
    {
        [Fact]
        public async Task TesteDoToken()
        {
            await AdicionarToken();
        }
    }
}
