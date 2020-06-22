using Microsoft.AspNetCore.Hosting;
using System;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Extensions.Host;

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
        /// <param name="webHostEnvironment">Ambiente de execução da aplicação</param>
        /// <returns>Instância da entitade leilão correspondente ao DTO usado na invocação do método</returns>
        public Auction To(User responsible, IWebHostEnvironment webHostEnvironment)
        {
            return new Auction
            {
                // Devido ao comportamento do cliente do Mongo, para mockar um objeto salvo é necessário determinar um ID de 24 caracteres na conversão
                Id = webHostEnvironment.IsTesting() ? "000000000000000000000000" : null,
                Close = Close,
                InitialBid = InitialBid,
                IsUsed = IsUsed,
                Name = Name,
                Open = Open,
                Responsible = new AuctionResponsibleUser
                {
                    Id = responsible?.Id,
                    Name = responsible?.Name
                }
            };
        }
    }
}
