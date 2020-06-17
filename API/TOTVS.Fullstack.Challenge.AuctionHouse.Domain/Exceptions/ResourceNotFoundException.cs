using System;
using System.Runtime.Serialization;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Constants;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions
{
    /// <summary>
    /// Exceção para recurso não encontrado
    /// </summary>
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        /// <summary>
        /// Mensagem da exceção
        /// </summary>
        public override string Message { get; }

        /// <summary>
        /// Tipo do recurso que não foi encontrado
        /// </summary>
        public Type ResourceType { get; }

        /// <summary>
        /// Identificador que foi utilizado na busca do recurso
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="resourceType">Tipo do recurso</param>
        /// <param name="id">Identificador usado na busca do recurso</param>
        public ResourceNotFoundException(Type resourceType, string id) : base()
        {
            ResourceType = resourceType;
            Id = id;
            Message = String.Format(ErrorMessage.ResourceNotFoundException, ResourceType.Name, Id);
        }

        /// <summary>
        /// Construtor de serialização
        /// </summary>
        /// <param name="info">Dados de serialização</param>
        /// <param name="context">Contexto de fluxo de dados</param>
        protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
