using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Mappings.MongoDb.Auctions
{
    /// <summary>
    /// Classe de mapeamento para o usuário responsável por um leilão
    /// </summary>
    public static class AuctionResponsibleUserMap
    {
        /// <summary>
        /// Configura o mapeamento da entidade AuctionResponsibleUser para o MongoDB
        /// </summary>
        public static void Configure()
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(AuctionResponsibleUser)))
            {
                return;
            }

            try
            {
                BsonClassMap.RegisterClassMap<AuctionResponsibleUser>(map =>
                {
                    map.AutoMap();
                    map.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                    map.SetIgnoreExtraElements(true);
                    map.MapIdProperty(auctionResponsibleUser => auctionResponsibleUser.Id);
                    map.MapMember(auctionResponsibleUser => auctionResponsibleUser.Name).SetIsRequired(true);
                });
            }
            catch
            {
                // Exceção ignorada porque a verificação no início do mapeamento não é suficiente
                // Isso é necessáiro somente para executar os testes em paralelo
                // Se deve ao fato do objeto de registo é estático, logo sofre de concorrência entre cada teste de API
            }
        }
    }
}
