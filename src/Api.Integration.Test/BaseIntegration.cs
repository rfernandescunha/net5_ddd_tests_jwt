using Api.Data.Context;
using Api.Domain.Dto;
using application;
using AutoMapper;
using crosscutting.AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test
{
    public abstract class BaseIntegration: IDisposable
    {
        public ApiContext apiContext { get; private set; }
        public HttpClient httpClient { get; private set; }
        public IMapper mapper { get; private set; }
        public string hostApi { get; set; }
        public HttpResponseMessage httpResponseMessage { get; set; }

        public BaseIntegration()
        {
            hostApi = "http://localhost:5000/api/v1/";

            var builder = new WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>();

            var server = new TestServer(builder);

            apiContext = server.Host.Services.GetService(typeof(ApiContext)) as ApiContext;
            apiContext.Database.Migrate();

            mapper = new AutoMapperFixture().GetMapper();

            httpClient = server.CreateClient();
        }

        public async Task AdicionarToken()
        {
            var loginDto = new LoginDto()
            {
                Email = "rafael.cunha@teste.com.br"
            };

            var resultLogin = await PostJsonAsync(loginDto, $"{hostApi}Login", httpClient);
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
            var loginObj = JsonConvert.DeserializeObject<LoginResponseDtoResult>(jsonLogin);

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginObj.acessToken);
        }

        public static async Task<HttpResponseMessage>PostJsonAsync(object dataclass, string url, HttpClient client)
        {
            return await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(dataclass), System.Text.Encoding.UTF8, "application/json"));
        }

        public void Dispose()
        {
            apiContext.Dispose();
            httpClient.Dispose();
        }
    }

    public class AutoMapperFixture: IDisposable
    {
        public void Dispose(){}

        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(x =>
            {
                x.AddProfile(new DtoToModelProfile());
                x.AddProfile(new EntityToDtoProfile());
                x.AddProfile(new ModelToEntityProfile());
            });

            return config.CreateMapper();
        }
    }
}
