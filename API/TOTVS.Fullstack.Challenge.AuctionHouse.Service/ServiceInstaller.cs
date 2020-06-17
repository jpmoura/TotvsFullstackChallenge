using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validators.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validators.Helper;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service
{
    public static class ServiceInstaller
    {
        public static void Install(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IUserService, UserService>();
            serviceCollection.AddSingleton<IAuctionService, AuctionService>();
            serviceCollection.AddSingleton<IJwtAuthenticationService, JwtAuthenticationService>();

            // Validadores
            serviceCollection.AddSingleton<IValidator<Pagination>, PaginationValidator>();
            serviceCollection.AddSingleton<IValidator<Auction>, AuctionValidator>();
            serviceCollection.AddSingleton<IValidator<AuctionResponsibleUser>, AuctionResponsibleUserValidator>();

        }
    }
}
