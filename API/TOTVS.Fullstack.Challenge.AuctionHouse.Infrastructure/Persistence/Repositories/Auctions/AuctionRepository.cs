﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Contexts;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Repositories;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Repositories.Auctions
{
    public class AuctionRepository : BaseMongoDbRepository<Auction>, IRepository<Auction>
    {
        protected override string CollectionName => "auctions";

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="mongoDbContext">Contexto do MongoDb</param>
        /// <param name="configuration">Configurações da aplicação</param>
        public AuctionRepository(IMongoDbContext mongoDbContext, IConfiguration configuration) : base(mongoDbContext, configuration) { }

        public async Task DeleteAsync(string id)
        {
            await GetCollection().DeleteOneAsync(auction => auction.Id == id);
        }

        public async Task<Auction> GetByIdAsync(string id)
        {
            IAsyncCursor<Auction> auction = await GetCollection().FindAsync(auction => auction.Id == id);
            return await auction.SingleOrDefaultAsync();
        }

        public async Task<Auction> UpdateAsync(Auction entityToUpdate)
        {
            return await GetCollection().FindOneAndReplaceAsync(auction => auction.Id == entityToUpdate.Id, entityToUpdate);
        }
    }
}
