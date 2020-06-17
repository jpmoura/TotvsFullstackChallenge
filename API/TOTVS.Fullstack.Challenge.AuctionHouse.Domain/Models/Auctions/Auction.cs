using System;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions
{
    /// <summary>
    /// Modelo da entidade leilão
    /// </summary>
    public class Auction
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
        public AuctionResponsibleUser Responsible { get; set; }
    }
}
