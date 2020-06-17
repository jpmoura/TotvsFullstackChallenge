using System.Linq;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Repositories
{
    /// <summary>
    /// Contrato de repositório
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Obtém uma lista paginada das instâncias da entidade
        /// </summary>
        /// <param name="pagination">Parâmetros de paginação</param>
        /// <returns>Lista paginada da entidade</returns>
        IQueryable<TEntity> List(Pagination pagination);

        /// <summary>
        /// Obtém uma entidade pelo ID assincronamente
        /// </summary>
        /// <param name="id">ID da entidade</param>
        /// <returns>Instância da entidade caso encontrada e null caso contrário</returns>
        Task<TEntity> GetByIdAsync(string id);

        /// <summary>
        /// Atualiza uma instância da entidade no banco de dados assincronamente
        /// </summary>
        /// <param name="entityToUpdate">Entidade a ser atualizada</param>
        /// <returns>Instância atualizada da entidade</returns>
        Task<TEntity> UpdateAsync(TEntity entityToUpdate);

        /// <summary>
        /// Cria uma nova instância da entidade no banco de dados assincronamente
        /// </summary>
        /// <param name="newEntity">Instância da entidade a ser criada</param>
        Task CreateAsync(TEntity newEntity);

        /// <summary>
        /// Remove a instância da entidade do banco de dados assincronamente
        /// </summary>
        /// <param name="id">ID da entidade</param>
        Task DeleteAsync(string id);
    }
}
