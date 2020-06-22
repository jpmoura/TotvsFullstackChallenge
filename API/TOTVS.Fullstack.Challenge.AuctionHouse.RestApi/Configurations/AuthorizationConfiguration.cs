using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Configurations
{
    /// <summary>
    /// Classe que aplica as configurações de autorização da aplicação
    /// </summary>
    public static class AuthorizationConfiguration
    {
        /// <summary>
        /// Aplica as configurações de autorização
        /// </summary>
        /// <param name="serviceCollection">Coleção de serviços da aplicação</param>
        public static void Apply(IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthorization(options =>
            {
                // Exige um usuário autenticado para todas as rotas por padrão
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });
        }
    }
}
