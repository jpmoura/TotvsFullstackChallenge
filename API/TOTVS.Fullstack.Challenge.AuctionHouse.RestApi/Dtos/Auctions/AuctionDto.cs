using System;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Auctions
{
    public class AuctionDto
    {
        /// <summary>
        /// ID do leilão
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Nome do leilão
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Lance inicial
        /// </summary>
        public double InitialBid { get; set; }

        /// <summary>
        /// Data de abertura
        /// </summary>
        public DateTime Open { get; set; }

        /// <summary>
        /// Data de encerramento
        /// </summary>
        public DateTime Close { get; set; }

        /// <summary>
        /// Indicador de item usado
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// Usuário responsável pelo leilão
        /// </summary>
        public AuctionResponsibleUserDto Responsible { get; set; }

        public static AuctionDto From(Auction entity)
        {
            return new AuctionDto
            {
                Close = entity.Close,
                Id = entity.Id,
                InitialBid = entity.InitialBid,
                IsUsed = entity.IsUsed,
                Name = entity.Name,
                Open = entity.Open,
                Responsible = AuctionResponsibleUserDto.From(entity.Responsible)
            };
        }

        /// <summary>
        /// Cria uma instância de DTO de leilão em uma instância da entidade leilão
        /// </summary>
        /// <param name="responsible">Instância do usuário responsável pelo leilão</param>
        /// <returns>Instância da entitade leilão correspondente ao DTO usado na invocação do método</returns>
        public Auction To(User responsible)
        {
            return new Auction
            {
                Close = Close,
                Id = Id,
                InitialBid = InitialBid,
                IsUsed = IsUsed,
                Name = Name,
                Open = Open,
                Responsible = new AuctionResponsibleUser
                {
                    Id = responsible.Id,
                    Name = responsible.Name
                }
            };
        }
    }
}
