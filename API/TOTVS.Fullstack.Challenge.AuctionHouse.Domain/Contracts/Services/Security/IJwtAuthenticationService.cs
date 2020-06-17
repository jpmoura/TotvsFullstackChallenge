using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Security;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Security
{
    /// <summary>
    /// Contrato de serviço de autenticação que produz um JWT
    /// </summary>
    public interface IJwtAuthenticationService
    {
        /// <summary>
        /// Autentica um usuário produzindo um JWT como resultado
        /// </summary>
        /// <param name="username">Nome do usuário no sistema</param>
        /// <param name="password">Senha do usuário</param>
        /// <returns>Modelo representando o usuário autenticado</returns>
        Task<JwtAuthenticationResult> AuthenticateAsync(string username, string password);
    }
}
