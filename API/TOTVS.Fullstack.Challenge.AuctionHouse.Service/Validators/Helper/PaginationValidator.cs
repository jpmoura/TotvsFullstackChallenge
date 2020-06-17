using FluentValidation;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validators.Helper
{
    /// <summary>
    /// Validador de parâmetros de paginação
    /// </summary>
    public class PaginationValidator : AbstractValidator<Pagination>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public PaginationValidator()
        {
            RuleFor(pagination => pagination.Page).Null().When(pagination => pagination.PageSize == null);
            RuleFor(pagination => pagination.Page).NotNull().When(pagination => pagination.PageSize != null).GreaterThan(0);
            RuleFor(pagination => pagination.PageSize).NotNull().When(pagination => pagination.Page != null).GreaterThan(0);
        }
    }
}
