using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Errors;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validations;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Controllers.Auctions
{
    /// <summary>
    /// Controller de leilão
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class AuctionController : ControllerBase
    {
        /// <summary>
        /// Serviço de leilão
        /// </summary>
        private readonly IAuctionService auctionService;

        /// <summary>
        /// Serviço de usuário
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Ambiente de execução da aplicação
        /// </summary>
        private readonly IWebHostEnvironment webHostEnvironment;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="auctionService">Serviço de leilão</param>
        /// <param name="userService">Serviço de usuário</param>
        /// <param name="webHostEnvironment">Ambiente de execução da aplicação</param>
        public AuctionController(IAuctionService auctionService, IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            this.auctionService = auctionService;
            this.userService = userService;
            this.webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Lista leilões
        /// </summary>
        /// <param name="pagination">Paginação</param>
        /// <returns>Lista paginada de leilões cadastrados</returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Leilões cadastrados", typeof(IEnumerable<AuctionDto>))]
        public ActionResult<IEnumerable<AuctionDto>> Index([FromQuery] Pagination pagination)
        {
            return Ok(auctionService.List(pagination).AsEnumerable().Select(auction => AuctionDto.From(auction)));
        }

        /// <summary>
        /// Obtém um leilão
        /// </summary>
        /// <param name="id">Identificador do leilão (Exatamente 24 caracteres)</param>
        /// <returns>Leilão encontrado</returns>
        [HttpGet("{id:length(24)}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Leilão encontrado", typeof(AuctionDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Leilão não encontrado", typeof(ErrorMessageDto))]
        public async Task<ActionResult<AuctionDto>> GetById(string id)
        {
            Auction auction = await auctionService.GetByIdAsync(id);

            CommonValidator.EnforceResourceFound(auction, typeof(Auction), id);

            return Ok(AuctionDto.From(auction));
        }

        /// <summary>
        /// Cria um leilão
        /// </summary>
        /// <param name="newAuctionDto">Modelo do leilão a ser criado</param>
        /// <param name="apiVersion">Versão da API. Adicionado automaticamente.</param>
        /// <returns>Leilão criado</returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Leilão criado com sucesso", typeof(AuctionDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Parâmetro(s) de entrada inválido(s)", typeof(ErrorMessageDto))]
        public async Task<ActionResult<AuctionDto>> Create([FromBody] CreateAuctionInputDto newAuctionDto, ApiVersion apiVersion)
        {
            CommonValidator.EnforceNotNull(newAuctionDto, nameof(newAuctionDto));

            User responsible = await userService.GetByIdAsync(newAuctionDto.ResponsibleUserId);

            if (responsible == null)
            {
                throw new InvalidParameterException(nameof(newAuctionDto.ResponsibleUserId));
            }

            Auction newAuction = newAuctionDto.To(responsible, webHostEnvironment);
            await auctionService.CreateAsync(newAuction);

            return CreatedAtAction(nameof(GetById), new { id = newAuction.Id, version = apiVersion.ToString() }, AuctionDto.From(newAuction));
        }

        /// <summary>
        /// Atualiza um leilão
        /// </summary>
        /// <param name="id">Identificador do leilão</param>
        /// <param name="auctionToUpdate">Modelo com as informações atualizadas</param>
        /// <returns>Modelo representando o leilão atualizado</returns>
        [HttpPut("{id:length(24)}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Leilão atualizado com sucesso", typeof(AuctionDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Parâmetro(s) de entrada inválido(s)", typeof(ErrorMessageDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Leilão não encontrado", typeof(ErrorMessageDto))]
        public async Task<IActionResult> Update(string id, [FromBody] AuctionDto auctionToUpdate)
        {
            Auction auction = await auctionService.GetByIdAsync(id);
            CommonValidator.EnforceResourceFound(auction, typeof(Auction), id);

            User responsible = await userService.GetByIdAsync(auctionToUpdate.Responsible?.Id);
            CommonValidator.EnforceResourceFound(responsible, typeof(User), nameof(auctionToUpdate.Responsible.Id));

            Auction updatedAuction = await auctionService.UpdateAsync(auctionToUpdate.To(responsible));

            return Ok(AuctionDto.From(updatedAuction));
        }

        /// <summary>
        /// Deleta um leilão
        /// </summary>
        /// <param name="id">Identificador do leilão</param>
        /// <returns>Confirmação de deleção</returns>
        [HttpDelete("{id:length(24)}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Leilão atualizado com sucesso", typeof(AuctionDto))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Leilão não encontrado", typeof(ErrorMessageDto))]
        public async Task<IActionResult> Delete(string id)
        {
            Auction auction = await auctionService.GetByIdAsync(id);

            CommonValidator.EnforceResourceFound(auction, typeof(Auction), id);

            await auctionService.DeleteAsync(auction.Id);

            return NoContent();
        }
    }
}
