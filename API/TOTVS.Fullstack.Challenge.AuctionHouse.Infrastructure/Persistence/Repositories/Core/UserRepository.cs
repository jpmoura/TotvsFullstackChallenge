using MongoDB.Driver;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Contexts;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Repositories.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Repositories.Core
{
    public class UserRepository : BaseMongoDbRepository<User>, IUserRepository
    {
        protected override string DatabaseName => "auctionHouse";

        protected override string CollectionName => "users";

        public UserRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext) { }

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
