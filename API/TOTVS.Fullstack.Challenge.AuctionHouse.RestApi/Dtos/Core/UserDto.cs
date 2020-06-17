using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Core
{
    /// <summary>
    /// DTO da entidade usuário
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// ID do usuário
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Obtém uma instância de DTO de usuário a partir de uma entidade
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>DTO de usuário</returns>
        public static UserDto From(User entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
