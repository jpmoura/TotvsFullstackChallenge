using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Security
{
    /// <summary>
    /// Contrato de serviço de autenticação
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authentica o usuário assincronamente
        /// </summary>
        /// <param name="username">Nome do usuário no sistema</param>
        /// <param name="password">Senha</param>
        /// <returns>Usuário autenticado</returns>
        Task<User> AuthenticateAsync(string username, string password);
    }
}
