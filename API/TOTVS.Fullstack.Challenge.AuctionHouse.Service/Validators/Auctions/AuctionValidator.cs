using FluentValidation;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Constants;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validators.Auctions
{
    /// <summary>
    /// Validador de leilão
    /// </summary>
    public class AuctionValidator : AbstractValidator<Auction>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="auctionResponsibleUserValidator">Validador de entidade de usuário responsável por leilão</param>
        public AuctionValidator(IValidator<AuctionResponsibleUser> auctionResponsibleUserValidator)
        {
            RuleSet(ValidationRuleSetName.Create, () =>
            {
                RuleFor(auction => auction.Id).Null();
            });

            RuleFor(auction => auction.InitialBid).GreaterThanOrEqualTo(0);
            RuleFor(auction => auction.Name).NotEmpty();
            RuleFor(auction => auction.Open).LessThan(auction => auction.Close);
            RuleFor(auction => auction.Responsible).NotNull().SetValidator(auctionResponsibleUserValidator);
        }
    }
}
