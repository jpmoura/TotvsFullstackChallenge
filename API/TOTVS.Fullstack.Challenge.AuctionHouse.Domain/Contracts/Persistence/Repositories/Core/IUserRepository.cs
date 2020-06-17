using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Repositories.Core
{
    /// <summary>
    /// Contrato do repositório de usuário
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Obtém um usuário pelo nome de sistema assincronamente
        /// </summary>
        /// <param name="username">Nome de sistema do usuário</param>
        /// <returns>Instância representando o usuário caso encontrado ou null caso contrário</returns>
        Task<User> GetByUsernameAsync(string username);
    }
}
