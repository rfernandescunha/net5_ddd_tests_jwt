using Api.Domain.Interfaces.Services;
using Api.Service.Services;
using domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using service.Services;

namespace Api.CrossCutting.DependencyInjection
{
    public static class InjectionService
    {
        public static void Register(IServiceCollection serviceCollection) 
        {
            serviceCollection.AddTransient<IUserService, UserService> ();

            serviceCollection.AddTransient<ILoginService, LoginService>();
        }
    }
}
