using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Installer;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Configurations;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Filters;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi
{
    /// <summary>
    /// Classe de inicialização da aplciação
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Inicializa a aplicação
        /// </summary>
        /// <param name="configuration">Configuração da aplicação</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuração da aplicação
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configura os serviços da aplicação
        /// </summary>
        /// <param name="services">Coleção de serviços</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddCors();

            services.AddControllers();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("obterUmaChaveDaConfiguracao")), // pegar string das configurações),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"DEU RUIM => {context.Exception.Message}");

                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        if (context?.AuthenticateFailure is SecurityTokenExpiredException)
                        {
                            string error = context.Error; // "invalid_token"
                            string errorDescription = context.ErrorDescription; // "The token is expired"
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(CustomExceptionFilter));
            });

            SwaggerConfiguration.Apply(services);

            InfrastructureInstaller.Install(services);
            ServiceInstaller.Install(services);
        }

        /// <summary>
        /// Configura o pipeline HTTP da API
        /// </summary>
        /// <param name="app">Construtor da aplicação</param>
        /// <param name="env">Ambiente do host</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            SwaggerConfiguration.Apply(app);

            app.UseRouting();

            app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
