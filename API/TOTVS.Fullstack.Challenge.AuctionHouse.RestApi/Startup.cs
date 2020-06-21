using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Installer;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Configurations;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Filters;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi
{
    /// <summary>
    /// Classe de inicializaçãoo da aplicaçãoo
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

            AuthenticationConfiguration.Apply(services, Configuration);

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
        /// <param name="configuration">Configurações da aplicação</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Boolean.TryParse(configuration["Network:UseHttps"], out bool useHttps);

            if (useHttps)
            {
                app.UseHttpsRedirection();
            }

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
