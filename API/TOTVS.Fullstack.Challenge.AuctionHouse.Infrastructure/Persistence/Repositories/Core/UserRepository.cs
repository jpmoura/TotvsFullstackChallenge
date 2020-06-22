using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Contexts;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Repositories.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Repositories.Core
{
    public class UserRepository : BaseMongoDbRepository<User>, IUserRepository
    {
        protected override string CollectionName => "users";

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="mongoDbContext">Contexto do MongoDb</param>
        /// <param name="configuration">Configurações da aplicação</param>
        public UserRepository(IMongoDbContext mongoDbContext, IConfiguration configuration) : base(mongoDbContext, configuration) { }

        public async Task DeleteAsync(string id)
        {
            await GetCollection().DeleteOneAsync(user => user.Id == id);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            IAsyncCursor<User> user = await GetCollection().FindAsync(user => user.Id == id);
            return await user.SingleOrDefaultAsync();
        }

        public async Task<User> UpdateAsync(User entityToUpdate)
        {
            return await GetCollection().FindOneAndReplaceAsync(user => user.Id == entityToUpdate.Id, entityToUpdate);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            IAsyncCursor<User> result = await GetCollection().FindAsync(user => user.Username == username);
            return await result.SingleOrDefaultAsync();
        }
    }
}
