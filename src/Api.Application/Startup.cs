using System;
using System.Collections.Generic;
using Api.CrossCutting.DependencyInjection;
using Api.Data.Context;
using AutoMapper;
using crosscutting.AutoMapper;
using domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;

        }

        public IConfiguration _configuration { get; }
        public IWebHostEnvironment _webHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InjectionService.Register(services);
            InjectionRepository.Register(services, _configuration);

            if (_webHostEnvironment.IsEnvironment("Testing"))
            {
                Environment.SetEnvironmentVariable("ConnectionString", "Data Source=.\\SQLEXPRESS;Initial Catalog=dbAPI_Integration;User ID=rfcunha;Password=1234567");
                Environment.SetEnvironmentVariable("Banco", "Sql");
                Environment.SetEnvironmentVariable("Migration", "True");
                Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
                Environment.SetEnvironmentVariable("Issuer", "ExmploIssuer");
                Environment.SetEnvironmentVariable("Seconds",  "28800");
            }

            #region Mapper

            var configAutoMapper = new AutoMapper.MapperConfiguration(x => {

                x.AddProfile(new DtoToModelProfile());
                x.AddProfile(new EntityToDtoProfile());
                x.AddProfile(new ModelToEntityProfile());
            
            });

            IMapper mapper = configAutoMapper.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            #region Token Jwt

            var signingConfiguration = new SigningConfiguration();
            services.AddSingleton(signingConfiguration); //Instancia Unica

            ////Usando do appsettings.json
            //var tokenConfigurations = new TokenConfiguration();
            //new ConfigureFromConfigurationOptions<TokenConfiguration>( _configuration.GetSection("TokenConfiguration")).Configure(tokenConfigurations);
            //services.AddSingleton(tokenConfigurations); //Instancia Unica

            services.AddAuthentication(authOptions => {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfiguration.Key;
                //paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
                //paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");

                // // Valida a assinatura de um token recebido
                // paramsValidation.ValidateIssuerSigningKey = true;

                // // Verifica se um token recebido ainda é válido
                // paramsValidation.ValidateLifetime = true;

                // // Tempo de tolerância para a expiração de um token (utilizado
                // // caso haja problemas de sincronismo de horário entre diferentes
                // // computadores envolvidos no processo de comunicação)
                // paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(x => {
                x.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            #endregion

            #region Swagger 

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Curso de Api AspNetCore",
                    Description = "API em DDD - Swagger",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Rafael F Cunha",
                        Email = "rafa_fernandes_cunha@hotmail.com"
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Termo de Licen�a",
                        Url = new Uri("http://www.teste.com.br")
                    }

                });

                x.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Entre com o Token Jwt",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
                    }

                });

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //x.IncludeXmlComments(xmlPath);
            });

            #endregion

            services.AddControllers();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso de Api AspNetCore");
                x.RoutePrefix = string.Empty;
            });

            //Realiza o Migration do banco de dados sem precisar executar command
            if (!_webHostEnvironment.IsEnvironment("Testing"))
            {
                if (_configuration.GetSection("SqlConfiguration").GetSection("Migration").Value.Equals("True"))
                {
                    using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        using (var context = service.ServiceProvider.GetService<ApiContext>())
                        {
                            context.Database.Migrate();
                        }
                    }
                }
            }
        }
    }
}
