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
            if (BsonClassMap.IsClassMapRegistered(typeof(User)))
            {
                return;
            }

            try
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
            catch
            {
                // Exceção ignorada porque a verificação no início do mapeamento não é suficiente
                // Isso é necessáiro somente para executar os testes em paralelo
                // Se deve ao fato do objeto de registo é estático, logo sofre de concorrência entre cada teste de API
            }
        }
    }
}
