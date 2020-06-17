using MongoDB.Driver;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Contexts;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Contexts
{
    /// <summary>
    /// Classe concreta de contexto do MongoDB
    /// </summary>
    public class MongoDbContext : IMongoDbContext
    {
        /// <summary>
        /// Cliente do MongoDB
        /// </summary>
        public readonly IMongoClient mongoClient;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="mongoClient"></param>
        public MongoDbContext(IMongoClient mongoClient)
        {
            this.mongoClient = mongoClient;
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string databaseName, string collectionName)
        {
            return mongoClient.GetDatabase(databaseName).GetCollection<TEntity>(collectionName);
        }
    }
}
