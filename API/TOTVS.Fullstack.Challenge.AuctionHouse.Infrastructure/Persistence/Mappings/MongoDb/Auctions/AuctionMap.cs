using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Mappings.MongoDb.Auctions
{
    /// <summary>
    /// Classe de mapeamento da entidade leilão
    /// </summary>
    public static class AuctionMap
    {
        /// <summary>
        /// Configura o mapeamento da entidade leilão para o MongoDB
        /// </summary>
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Auction>(map =>
            {
                map.AutoMap();
                map.MapIdMember(auction => auction.Id);
                map.SetIgnoreExtraElements(true);
                map.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId)).SetIdGenerator(StringObjectIdGenerator.Instance);
                map.MapMember(auction => auction.Name).SetIsRequired(true);
                map.MapMember(auction => auction.InitialBid).SetIsRequired(true);
                map.MapMember(auction => auction.Open).SetIsRequired(true);
                map.MapMember(auction => auction.Close).SetIsRequired(true);
                map.MapMember(auction => auction.IsUsed).SetIsRequired(true);
                map.MapMember(auction => auction.Responsible).SetIsRequired(true);
            });
        }
    }
}
