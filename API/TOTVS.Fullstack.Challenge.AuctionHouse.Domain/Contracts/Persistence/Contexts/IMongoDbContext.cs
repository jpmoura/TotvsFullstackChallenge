using MongoDB.Driver;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Contexts
{
    /// <summary>
    /// Contrato de contexto do MongoDB
    /// </summary>
    public interface IMongoDbContext
    {
        /// <summary>
        /// Obtém uma coleção do MongoDB relacionada a um tipo de entidade
        /// </summary>
        /// <typeparam name="T">Tipo da entidade</typeparam>
        /// <param name="databaseName">Nome da base de dados</param>
        /// <param name="collectionName">Nome da coleção</param>
        /// <returns>Coleção relacionada à entidade</returns>
        IMongoCollection<T> GetCollection<T>(string databaseName, string collectionName);
    }
}
