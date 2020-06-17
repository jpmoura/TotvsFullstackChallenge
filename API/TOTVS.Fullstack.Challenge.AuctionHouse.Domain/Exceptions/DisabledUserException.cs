using System;
using System.Runtime.Serialization;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Constants;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions
{
    /// <summary>
    /// Exceção lançada quando a ação exige que o usuário esteja ativado para ser executada
    /// </summary>
    [Serializable]
    public class DisabledUserException : Exception
    {
        /// <summary>
        /// Mensagem da exceção
        /// </summary>
        public override string Message { get; }

        /// <summary>
        /// Usuário que está desativado
        /// </summary>
        public User User { get; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="user">Usuário que está desativado</param>
        public DisabledUserException(User user) : base()
        {
            User = user;
            Message = String.Format(ErrorMessage.DisabledUserException, User.Id);
        }

        /// <summary>
        /// Construtor de serialização
        /// </summary>
        /// <param name="info">Dados de serialização</param>
        /// <param name="context">Contexto de fluxo de dados</param>
        protected DisabledUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
