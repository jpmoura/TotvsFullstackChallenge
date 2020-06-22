using FluentValidation;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Extensions.Http;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Factories;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Test.Controllers.Core
{
    public class UserControllerTest : BaseTestController
    {
        public UserControllerTest(RestApiWebApplicationFactory restApiWebApplicationFactory) : base(restApiWebApplicationFactory)
        {
            endpointUri = "api/v1/User";
        }

        public class Authenticate : UserControllerTest
        {
            public Authenticate(RestApiWebApplicationFactory restApiWebApplicationFactory) : base(restApiWebApplicationFactory) { }

            [Fact]
            public async Task PayloadNulo_RertornaBadRequest()
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{endpointUri}/Authenticate", (AuthenticationInputDto)null);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                jwtAuthenticationServiceMock.Verify(mock => mock.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task PayloadPreenchido_UsuarioOuSenhaInvalidos_RetornaBadRequest(AuthenticationInputDto authenticationInputDto)
            {
                jwtAuthenticationServiceMock.Setup(mock => mock.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new InvalidParameterException(string.Empty));

                HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{endpointUri}/Authenticate", authenticationInputDto);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                jwtAuthenticationServiceMock.Verify(mock => mock.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task PayloadPreenchido_UsuarioSenhaValidos_UsuarioNaoEncontrado_RetornaBadRequest(AuthenticationInputDto authenticationInputDto)
            {
                jwtAuthenticationServiceMock.Setup(mock => mock.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new ResourceNotFoundException(typeof(User), string.Empty));

                HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{endpointUri}/Authenticate", authenticationInputDto);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                jwtAuthenticationServiceMock.Verify(mock => mock.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task PayloadPreenchido_UsuarioSenhaValidos_UsuarioEncontrado_UsuarioDesabilitado_RetornaUnauthorized(AuthenticationInputDto authenticationInputDto, User user)
            {
                jwtAuthenticationServiceMock.Setup(mock => mock.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new DisabledUserException(user));

                HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{endpointUri}/Authenticate", authenticationInputDto);

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
                jwtAuthenticationServiceMock.Verify(mock => mock.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task PayloadPreenchido_UsuarioSenhaValidos_UsuarioEncontrado_UsuarioHabilitado_RetornaOk(AuthenticationInputDto authenticationInputDto, JwtAuthenticationResult jwtAuthenticationResult)
            {
                jwtAuthenticationServiceMock.Setup(mock => mock.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(jwtAuthenticationResult);

                HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{endpointUri}/Authenticate", authenticationInputDto);
                bool wasDeserializationSuccessful = response.Content.TryGetContentValue(out JwtAuthenticationResultDto jwtAuthenticationResultDtoReturned);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(wasDeserializationSuccessful);
                Assert.NotNull(jwtAuthenticationResultDtoReturned);
                jwtAuthenticationServiceMock.Verify(mock => mock.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            }
        }

        public class Index : UserControllerTest
        {
            public Index(RestApiWebApplicationFactory restApiWebApplicationFactory) : base(restApiWebApplicationFactory) { }

            [Fact]
            public async Task PaginacaoInvalida_RetornaBadRequest()
            {
                userServiceMock.Setup(mock => mock.List(It.IsAny<Pagination>())).Throws(new ValidationException(string.Empty));

                HttpResponseMessage response = await httpClient.GetAsync(endpointUri);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                userServiceMock.Verify(mock => mock.List(It.IsAny<Pagination>()), Times.Once);
            }

            [Fact]
            public async Task PaginacaoValida_NenhumUsuarioEncontrado_RetornaOkListaVazia()
            {
                IQueryable<User> users = Enumerable.Empty<User>().AsQueryable();
                userServiceMock.Setup(mock => mock.List(It.IsAny<Pagination>())).Returns(users);

                HttpResponseMessage response = await httpClient.GetAsync(endpointUri);
                bool wasDeserializationSuccessful = response.Content.TryGetContentValue(out IEnumerable<UserDto> userDtosReturned);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(wasDeserializationSuccessful);
                Assert.Empty(userDtosReturned);
                userServiceMock.Verify(mock => mock.List(It.IsAny<Pagination>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task PaginacaoValida_UsuarioEncontrados_RetornaOkListaPreenchida(IEnumerable<User> users)
            {
                userServiceMock.Setup(mock => mock.List(It.IsAny<Pagination>())).Returns(users.AsQueryable());

                HttpResponseMessage response = await httpClient.GetAsync(endpointUri);
                bool wasDeserializationSuccessful = response.Content.TryGetContentValue(out IEnumerable<UserDto> userDtosReturned);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(wasDeserializationSuccessful);
                Assert.NotEmpty(userDtosReturned);
                userServiceMock.Verify(mock => mock.List(It.IsAny<Pagination>()), Times.Once);
            }
        }
    }
}
