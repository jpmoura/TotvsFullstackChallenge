using System;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validations
{
    /// <summary>
    /// Validator comum entre entidades
    /// </summary>
    public static class CommonValidator
    {
        /// <summary>
        /// Valida se um recurso foi encontrado
        /// </summary>
        /// <param name="instance">Instância do recurso recuperada</param>
        /// <param name="instanceType">Tipo da instância</param>
        /// <param name="instanceId">Identificador usado na recuperação da instância</param>
        public static void EnforceResourceFound(object instance, Type instanceType, string instanceId)
        {
            InternalEnforceNotNull(instance, new ResourceNotFoundException(instanceType, instanceId));
        }

        /// <summary>
        /// Valida se uma cadeia de caracteres não é nula, não está vazia e nem é composta por espaços em branco
        /// </summary>
        /// <param name="parameter">Parâmetro</param>
        /// <param name="parameterName">Nome original do parâmetro</param>
        public static void EnforceNotEmpty(string parameter, string parameterName)
        {
            if (String.IsNullOrWhiteSpace(parameter))
            {
                throw new InvalidParameterException(parameterName);
            }
        }

        /// <summary>
        /// Valida se um parâmetro não é nulo
        /// </summary>
        /// <param name="parameter">Instância do parâmetro</param>
        /// <param name="parameterName">Nome original do parâmetro</param>
        public static void EnforceNotNull(object parameter, string parameterName)
        {
            InternalEnforceNotNull(parameter, new InvalidParameterException(parameterName));
        }

        /// <summary>
        /// Método interno de validação de entidade não nula
        /// </summary>
        /// <param name="instance">Instância a ser validada</param>
        /// <param name="onNullException">Exceção a ser lançada em caso de erro de validação</param>
        private static void InternalEnforceNotNull(object instance, Exception onNullException)
        {
            if (instance == null)
            {
                throw onNullException;
            }
        }
    }
}
