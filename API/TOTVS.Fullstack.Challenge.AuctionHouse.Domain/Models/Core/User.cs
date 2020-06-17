namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core
{
    /// <summary>
    /// Modelo da entidade usuário
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID do objeto
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nome do usuário de sistema
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Indicador que define se o usuário está ativado ou não
        /// </summary>
        public bool IsActive { get; set; }
    }
}
