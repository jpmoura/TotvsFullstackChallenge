using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Auctions
{
    /// <summary>
    /// DTO da entidade usuário responsável pelo leilão
    /// </summary>
    public class AuctionResponsibleUserDto
    {
        /// <summary>
        /// ID do usuário responsável
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome do usuário responsável
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cria um DTO de usuário responsável pelo leilão a partir de uma instância da entidade
        /// </summary>
        /// <param name="entity">Instância da entidade usuário resposável pelo leilão</param>
        /// <returns>DTO de usuário responsável pelo leilão</returns>
        public static AuctionResponsibleUserDto From(AuctionResponsibleUser entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new AuctionResponsibleUserDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
