namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper
{
    /// <summary>
    /// Modelo de paginação
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Numeração da página
        /// </summary>
        public int? Page { get; set; } = null;

        /// <summary>
        /// Tamanho da página
        /// </summary>
        public int? PageSize { get; set; } = null;
    }
}
