using System;
using System.Runtime.Serialization;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Constants;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions
{
    /// <summary>
    /// Exceção lançada quando um parâmetro que passar por validação for inválido
    /// </summary>
    [Serializable]
    public class InvalidParameterException : Exception
    {
        /// <summary>
        /// Mensagem da exceção
        /// </summary>
        public override string Message { get; }

        /// <summary>
        /// Nome do parâmetro inválido
        /// </summary>
        public string ParameterName { get; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        /// <param name="parameterName">Tipo do recurso</param>
        public InvalidParameterException(string parameterName) : base()
        {
            ParameterName = parameterName;
            Message = String.Format(ErrorMessage.InvalidParameterException, ParameterName);
        }

        /// <summary>
        /// Construtor de serialização
        /// </summary>
        /// <param name="info">Dados de serialização</param>
        /// <param name="context">Contexto de fluxo de dados</param>
        protected InvalidParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
