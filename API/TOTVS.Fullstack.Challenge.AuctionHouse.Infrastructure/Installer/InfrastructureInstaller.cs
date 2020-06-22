using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Contexts;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Repositories;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Repositories.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Contexts;
using TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Mappings.MongoDb;
using TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Repositories.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Repositories.Core;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Installer
{
    /// <summary>
    /// Instalador de dependências da camada de infraestrutura
    /// </summary>
    public static class InfrastructureInstaller
    {
        /// <summary>
        /// Instala todas as dependências de infraestrutura
        /// </summary>
        /// <param name="serviceCollection">Coleção de serviços da aplicação</param>
        /// <param name="configuration">Configurações da aplicação</param>
        public static void Install(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            MongoDbMappingsInstaller.Install();
            InstallPersistence(serviceCollection, configuration);
        }

        /// <summary>
        /// Instala todas as dependências relacionadas à persistência
        /// </summary>
        /// <param name="serviceCollection">Coleção de serviços da aplicação</param>
        /// <param name="configuration">Configurações da aplicação</param>
        public static void InstallPersistence(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Contexto
            serviceCollection.AddSingleton<IMongoDbContext, MongoDbContext>();

            // Repos
            serviceCollection.AddSingleton<IRepository<Auction>, AuctionRepository>();
            serviceCollection.AddSingleton<IUserRepository, UserRepository>();

            // Clientes
            serviceCollection.AddSingleton<IMongoClient>(new MongoClient(configuration["MongoDb:ConnectionString"]));
        }
    }
}
