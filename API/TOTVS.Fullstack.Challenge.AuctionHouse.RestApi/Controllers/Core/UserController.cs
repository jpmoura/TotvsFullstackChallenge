using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Errors;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validations;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Controllers.Core
{
    /// <summary>
    /// Controller de usuário
    /// </summary>
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Serviço de usuário
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Serviço de autenticação com JWT
        /// </summary>
        private readonly IJwtAuthenticationService jwtAuthenticationService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="userService">Serviço de usuário</param>
        /// <param name="jwtAuthenticationService">Serviço de autenticação com JWT</param>
        public UserController(IUserService userService, IJwtAuthenticationService jwtAuthenticationService)
        {
            this.userService = userService;
            this.jwtAuthenticationService = jwtAuthenticationService;
        }

        /// <summary>
        /// Autentica um usuário
        /// </summary>
        /// <param name="authenticationInputDto">Modelo de entrada de dados de autenticação</param>
        [AllowAnonymous]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, "Usuário atenticado", typeof(JwtAuthenticationResultDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Parâmetro(s) de entrada inválido(s)", typeof(ErrorMessageDto))]
        [Route("Authenticate")]
        public async Task<ActionResult<JwtAuthenticationResultDto>> Authenticate(AuthenticationInputDto authenticationInputDto)
        {
            CommonValidator.EnforceNotNull(authenticationInputDto, nameof(authenticationInputDto));

            JwtAuthenticationResult jwtAuthenticationResult = await jwtAuthenticationService.AuthenticateAsync(authenticationInputDto.Username, authenticationInputDto.Password);

            return Ok(JwtAuthenticationResultDto.From(jwtAuthenticationResult));
        }

        /// <summary>
        /// Lista os usuários de maneira paginada
        /// </summary>
        /// <param name="pagination">Parâmetros de paginação</param>
        /// <returns>Lista paginada de usuários cadastrados</returns>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Leilões cadastrados", typeof(IEnumerable<UserDto>))]
        public IActionResult Index(Pagination pagination)
        {
            return Ok(userService.List(pagination).AsEnumerable().Select(user => UserDto.From(user)));
        }
    }
}
