using System.Linq;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Core
{
    /// <summary>
    /// Contrato de serviços de usuário
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Obtém uma instância da entidade de usuário pelo seu ID assincronamente
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>A instância do usuário encontrada ou null caso contrário</returns>
        public Task<User> GetByIdAsync(string id);

        /// <summary>
        /// Obtém um uusário baseado no seu nome de usuário de sistema
        /// </summary>
        /// <param name="username">Nome do usuário no sistema</param>
        /// <returns>O usuário caso encontrado ou null caso contrário</returns>
        public Task<User> GetByUsernameAsync(string username);

        /// <summary>
        /// Lista de forma paginada os usuários cadastrados
        /// </summary>
        /// <param name="pagination">Parâmetros de paginação</param>
        /// <returns>Lista de usuários "queryável"</returns>
        public IQueryable<User> List(Pagination pagination);
    }
}
