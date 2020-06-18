using System;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Auctions
{
    /// <summary>
    /// Modelo de entrada de dados para criação de leilão
    /// </summary>
    public class CreateAuctionInputDto
    {
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
        /// Identificador do usuário responsável
        /// </summary>
        public string ResponsibleUserId { get; set; }

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
