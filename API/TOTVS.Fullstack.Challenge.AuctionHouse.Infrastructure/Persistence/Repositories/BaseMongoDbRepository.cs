using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Contexts;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Classe base de repositório
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade relacionada ao repositório</typeparam>
    public class BaseMongoDbRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Contexto de banco de dados
        /// </summary>
        private readonly IMongoDbContext mongoDbContext;

        /// <summary>
        /// Nome da base de dados
        /// </summary>
        protected virtual string DatabaseName => throw new NotImplementedException(); // pegar do arquivo de configuração

        /// <summary>
        /// Propriedade que define o nome da coleção
        /// </summary>
        protected virtual string CollectionName => throw new NotImplementedException();

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="mongoDbContext">Contexto do MongoDB</param>
        public BaseMongoDbRepository(IMongoDbContext mongoDbContext)
        {
            this.mongoDbContext = mongoDbContext;
        }

        public IMongoCollection<TEntity> GetCollection()
        {
            return mongoDbContext.GetCollection<TEntity>(DatabaseName, CollectionName);
        }

        public IQueryable<TEntity> List(Pagination pagination)
        {
            MongoDB.Driver.Linq.IMongoQueryable<TEntity> collection = GetCollection().AsQueryable();

            if (pagination.Page.HasValue && pagination.PageSize.HasValue)
            {
                collection.Skip(pagination.Page.Value * pagination.PageSize.Value);
                collection.Take(pagination.PageSize.Value);
            }

            return collection;
        }

        public async Task CreateAsync(TEntity newEntity)
        {
            await GetCollection().InsertOneAsync(newEntity);
        }
    }
}
