using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Installer;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Configurations;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Filters;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi
{
    /// <summary>
    /// Classe de inicializa��o da aplcia��o
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Inicializa a aplica��o
        /// </summary>
        /// <param name="configuration">Configura��o da aplica��o</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configura��o da aplica��o
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configura os servi�os da aplica��o
        /// </summary>
        /// <param name="services">Cole��o de servi�os</param>
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
        /// <param name="app">Construtor da aplica��o</param>
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
