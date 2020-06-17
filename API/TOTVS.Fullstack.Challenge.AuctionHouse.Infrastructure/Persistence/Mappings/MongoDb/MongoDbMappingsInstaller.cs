using MongoDB.Bson.Serialization.Conventions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Mappings.MongoDb.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Mappings.MongoDb.Core;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Mappings.MongoDb
{
    /// <summary>
    /// Instalador de mapemaentos dos modelos para o MongoDB
    /// </summary>
    public static class MongoDbMappingsInstaller
    {
        /// <summary>
        /// Instala os mapeamentos dos modelos para o MongoDB
        /// </summary>
        public static void Install()
        {
            // Define como Camel Case o nome das chaves de cada registro no banco de dados
            ConventionPack conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            AuctionMap.Configure();
            AuctionResponsibleUserMap.Configure();
            UserMap.Configure();
        }
    }
}
