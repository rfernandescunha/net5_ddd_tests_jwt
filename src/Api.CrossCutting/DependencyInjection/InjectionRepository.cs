using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Repository;
using data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Api.CrossCutting.DependencyInjection
{
    public static class InjectionRepository
    {
        public static void Register(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            serviceCollection.AddScoped<IUserRepository, UserRepository>();

            //Pega a Conexao do arquivo lauch.json
            if(Environment.GetEnvironmentVariable("Banco") == "Sql")
            {
                serviceCollection.AddDbContext<ApiContext>(
                    options => options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString")));
            }


            ////Pega a conexão do appsettings.Development.json
            //var sqlDbSettings = configuration.GetSection("SqlConfiguration");

            //serviceCollection.AddDbContext<ApiContext>(
            //    options => options.UseSqlServer(sqlDbSettings.GetSection("ConnectionString").Value)
            //);
        }
    }
}
