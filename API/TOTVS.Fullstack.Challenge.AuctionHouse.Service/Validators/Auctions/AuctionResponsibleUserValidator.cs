using FluentValidation;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validators.Auctions
{
    /// <summary>
    /// Validador do modelo de usuário responsável por leilão
    /// </summary>
    public class AuctionResponsibleUserValidator : AbstractValidator<AuctionResponsibleUser>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public AuctionResponsibleUserValidator()
        {
            RuleFor(auctionResponsibleUser => auctionResponsibleUser.Id).NotEmpty();
            RuleFor(auctionResponsibleUser => auctionResponsibleUser.Name).NotEmpty();
        }
    }
}
