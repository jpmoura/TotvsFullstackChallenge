using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Controllers.Core
{
    /// <summary>
    /// Controller de usuário
    /// </summary>
    [Authorize]
    [ApiController]
    [ApiVersion("1")]
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
        /// Serviço de log
        /// </summary>
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="userService">Serviço de usuário</param>
        /// <param name="jwtAuthenticationService">Serviço de autenticação com JWT</param>
        /// <param name="logger">Serviço de log</param>
        public UserController(IUserService userService, IJwtAuthenticationService jwtAuthenticationService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.jwtAuthenticationService = jwtAuthenticationService;
            this.logger = logger;
        }

        /// <summary>
        /// Autentica um usuário
        /// </summary>
        /// <param name="username">Nome de usuário no sistema</param>
        /// <param name="password">Senha do usuário</param>
        [AllowAnonymous]
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, "Usuário atenticado", typeof(JwtAuthenticationResultDto))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Parâmetro(s) de entrada inválido(s)", typeof(ErrorMessageDto))]
        [Route("Authenticate")]
        public async Task<ActionResult<JwtAuthenticationResultDto>> Authenticate(string username, string password)
        {
            JwtAuthenticationResult jwtAuthenticationResult = await jwtAuthenticationService.AuthenticateAsync(username, password);

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
