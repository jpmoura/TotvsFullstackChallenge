using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Persistence.Mappings.MongoDb.Core
{
    /// <summary>
    /// Classe de mapeamento da entidae usuário
    /// </summary>
    public static class UserMap
    {
        /// <summary>
        /// Configura o mapeamento da entidade usuário para o MongoDB
        /// </summary>
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.AutoMap();
                map.MapIdMember(user => user.Id);
                map.SetIgnoreExtraElements(true);
                map.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId)).SetIdGenerator(StringObjectIdGenerator.Instance);
                map.MapMember(user => user.Username).SetIsRequired(true);
                map.MapMember(user => user.Password).SetIsRequired(true);
                map.MapMember(user => user.IsActive).SetIsRequired(true);
                map.MapMember(user => user.Name).SetIsRequired(true);
            });
        }
    }
}
