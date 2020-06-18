namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Security
{
    /// <summary>
    /// Modelo de entrada de dados para autenticação
    /// </summary>
    public class AuthenticationInputDto
    {
        /// <summary>
        /// Nome do usuário no sistema
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Password { get; set; }
    }
}
