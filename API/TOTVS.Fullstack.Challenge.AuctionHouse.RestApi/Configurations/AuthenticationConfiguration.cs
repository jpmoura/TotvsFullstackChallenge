using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Configurations
{
    /// <summary>
    /// Classe de configuração de autenticação da aplicação
    /// </summary>
    public static class AuthenticationConfiguration
    {
        /// <summary>
        /// Aplica as configurações de autenticação
        /// </summary>
        /// <param name="serviceCollection">Coleção de serviços da aplicação</param>
        /// <param name="configuration">Configrações da aplicação</param>
        public static void Apply(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ILogger<Startup> logger = serviceCollection.BuildServiceProvider().GetService<ILogger<Startup>>();

            serviceCollection.AddAuthentication(options =>
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Security:Secret"])),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        logger.LogError($"Error during authentication: {context.Exception.Message}");

                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        if (context.AuthenticateFailure != null)
                        {
                            logger.LogError($"Exception on Authentication Challenge: {context.AuthenticateFailure}");
                        }

                        logger.LogError($"Exception on Authentication Challenge: {context.AuthenticateFailure}");

                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
