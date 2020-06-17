using System;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Security;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Security
{
    /// <summary>
    /// Modelo de dados representando o resultado da autenticação que tem como produto um JWT
    /// </summary>
    public class JwtAuthenticationResultDto
    {
        /// <summary>
        /// JWT
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Data e horário de expiração do JWT
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nome de sistema do usuário
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// ID do usuário no sistema
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Cria um DTO de autenticação baseado em um token resultante do processo de autenticação
        /// </summary>
        /// <param name="jwtAuthenticationResult">Entidade de resultado de autenticação que tem como produto um JWT</param>
        /// <returns>DTO de resultado da autenticação</returns>
        public static JwtAuthenticationResultDto From(JwtAuthenticationResult jwtAuthenticationResult)
        {
            return new JwtAuthenticationResultDto
            {
                Expires = jwtAuthenticationResult.Expires,
                Name = jwtAuthenticationResult.Name,
                Token = jwtAuthenticationResult.Token,
                UserId = jwtAuthenticationResult.UserId,
                Username = jwtAuthenticationResult.Username
            };
        }
    }
}
