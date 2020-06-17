using System.Linq;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Auctions
{
    /// <summary>
    /// Contrato de serviços de leilão
    /// </summary>
    public interface IAuctionService
    {
        /// <summary>
        /// Lista os leilões
        /// </summary>
        /// <param name="pagination">Parâmetros de paginação</param>
        /// <returns>Uma lista de leilões cadastrados</returns>
        IQueryable<Auction> List(Pagination pagination);

        /// <summary>
        /// Obtém um leilão de acordo com o ID assincronamente
        /// </summary>
        /// <param name="id">ID do leilão</param>
        /// <returns>Instância do leilão caso encontrado e null caso contrário</returns>
        Task<Auction> GetByIdAsync(string id);

        /// <summary>
        /// Atualiza uma instância de leilão no banco de dados assincronamente
        /// </summary>
        /// <param name="auctionToUpdate">Leilão a ser atualizado</param>
        /// <returns>Leiláo atualizado</returns>
        Task<Auction> UpdateAsync(Auction auctionToUpdate);

        /// <summary>
        /// Cria uma nova instância de leilão no banco de dados assincronamente
        /// </summary>
        /// <param name="newAuction">Novo leilão</param>
        /// <returns>Tarefa assíncrona</returns>
        Task CreateAsync(Auction newAuction);

        /// <summary>
        /// Remove a instância de leilão do banco de dados assincronamente
        /// </summary>
        /// <param name="id">ID do leilão</param>
        /// <returns>Tarefa assíncrona</returns>
        Task DeleteAsync(string id);
    }
}
