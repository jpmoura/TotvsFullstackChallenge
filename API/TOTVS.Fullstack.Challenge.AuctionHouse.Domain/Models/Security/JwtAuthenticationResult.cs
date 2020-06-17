using System;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Security
{
    /// <summary>
    /// Modelo do resultado de uma autenticação que tem como produto um JWT
    /// </summary>
    public class JwtAuthenticationResult
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
    }
}
