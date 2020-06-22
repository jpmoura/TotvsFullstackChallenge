using Microsoft.AspNetCore.Hosting;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Extensions.Host
{
    /// <summary>
    /// Classe que agrupa os métodos de extensão relacionados à interface de WebHostEnvironment
    /// </summary>
    public static class WebHostEnvironmentExtensions
    {
        /// <summary>
        /// Define se o ambiente de execução da aplicação é um ambiente de teste
        /// </summary>
        /// <param name="env">Ambiente do WebHost</param>
        /// <returns>True se for um ambiente de teste e false caso contrário</returns>
        public static bool IsTesting(this IWebHostEnvironment env)
        {
            return env.EnvironmentName.ToLowerInvariant() == Domain.Constants.EnvironmentName.Testing.ToLowerInvariant();
        }
    }
}
