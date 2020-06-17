using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Constants;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Repositories;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validations;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Auctions
{
    /// <summary>
    /// Classe de serviço de leilão
    /// </summary>
    public class AuctionService : IAuctionService
    {
        /// <summary>
        /// Repositório de leilão
        /// </summary>
        private readonly IRepository<Auction> auctionRepository;

        /// <summary>
        /// Validador de paginação
        /// </summary>
        private readonly IValidator<Pagination> paginationValidator;

        /// <summary>
        /// Validador de leilão
        /// </summary>
        private readonly IValidator<Auction> auctionValidator;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="auctionRepository">Repositório de leilão</param>
        /// <param name="paginationValidator">Validador de paginação</param>
        /// <param name="auctionValidator">Validador de leilão</param>
        public AuctionService(IRepository<Auction> auctionRepository, IValidator<Pagination> paginationValidator, IValidator<Auction> auctionValidator)
        {
            this.auctionRepository = auctionRepository;
            this.paginationValidator = paginationValidator;
            this.auctionValidator = auctionValidator;
        }

        public async Task CreateAsync(Auction newAuction)
        {
            CommonValidator.EnforceNotNull(newAuction, nameof(newAuction));
            auctionValidator.Validate(newAuction, ruleSet: $"default,{ValidationRuleSetName.Create}");
            await auctionRepository.CreateAsync(newAuction);
        }

        public async Task DeleteAsync(string id)
        {
            CommonValidator.EnforceNotEmpty(id, nameof(id));
            await auctionRepository.DeleteAsync(id);
        }

        public async Task<Auction> GetByIdAsync(string id)
        {
            CommonValidator.EnforceNotEmpty(id, nameof(id));
            return await auctionRepository.GetByIdAsync(id);
        }

        public IQueryable<Auction> List(Pagination pagination)
        {
            paginationValidator.ValidateAndThrow(pagination);
            return auctionRepository.List(pagination);
        }

        public async Task<Auction> UpdateAsync(Auction auctionToUpdate)
        {
            CommonValidator.EnforceNotNull(auctionToUpdate, nameof(auctionToUpdate));
            auctionValidator.ValidateAndThrow(auctionToUpdate);
            return await auctionRepository.UpdateAsync(auctionToUpdate);
        }
    }
}
