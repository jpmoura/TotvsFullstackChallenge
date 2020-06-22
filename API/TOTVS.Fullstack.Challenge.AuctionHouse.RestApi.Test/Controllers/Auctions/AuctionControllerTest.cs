using AutoFixture.Xunit2;
using FluentValidation;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Infrastructure.Extensions.Http;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Factories;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Test.Controllers.Auctions
{
    public class AuctionControllerTest : BaseTestController
    {
        public AuctionControllerTest(RestApiWebApplicationFactory restApiWebApplicationFactory) : base(restApiWebApplicationFactory)
        {
            endpointUri = "api/v1/Auction";
        }

        public class Index : AuctionControllerTest
        {
            public Index(RestApiWebApplicationFactory restApiWebApplicationFactory) : base(restApiWebApplicationFactory) { }

            [Fact]
            public async Task PaginacaoInvalida_RetornaBadRequest()
            {
                auctionServiceMock.Setup(mock => mock.List(It.IsAny<Pagination>())).Throws(new ValidationException(string.Empty));

                HttpResponseMessage response = await httpClient.GetAsync(endpointUri);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                auctionServiceMock.Verify(mock => mock.List(It.IsAny<Pagination>()), Times.Once);
            }

            [Fact]
            public async Task PaginacaoValida_NenhumLeilaoEncontrado_RetornaOkListaVazia()
            {
                IQueryable<Auction> auctions = Enumerable.Empty<Auction>().AsQueryable();
                auctionServiceMock.Setup(mock => mock.List(It.IsAny<Pagination>())).Returns(auctions);

                HttpResponseMessage response = await httpClient.GetAsync(endpointUri);
                bool wasDeserializationSuccessful = response.Content.TryGetContentValue(out IEnumerable<AuctionDto> auctionDtosReturned);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(wasDeserializationSuccessful);
                Assert.Empty(auctionDtosReturned);
                auctionServiceMock.Verify(mock => mock.List(It.IsAny<Pagination>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task PaginacaoValida_LeiloesEncontrados_RetornaOkListaPreenchida(IEnumerable<Auction> auctions)
            {
                auctionServiceMock.Setup(mock => mock.List(It.IsAny<Pagination>())).Returns(auctions.AsQueryable());

                HttpResponseMessage response = await httpClient.GetAsync(endpointUri);
                bool wasDeserializationSuccessful = response.Content.TryGetContentValue(out IEnumerable<AuctionDto> auctionDtosReturned);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(wasDeserializationSuccessful);
                Assert.NotEmpty(auctionDtosReturned);
                auctionServiceMock.Verify(mock => mock.List(It.IsAny<Pagination>()), Times.Once);
            }
        }

        public class GetById : AuctionControllerTest
        {
            public GetById(RestApiWebApplicationFactory restApiWebApplicationFactory) : base(restApiWebApplicationFactory) { }

            [Theory, AutoData]
            public async Task IdInvalido_RetornaBadRequest(string id)
            {
                auctionServiceMock.Setup(mock => mock.GetByIdAsync(It.IsAny<string>())).ThrowsAsync(new ValidationException(string.Empty));

                HttpResponseMessage response = await httpClient.GetAsync($"{endpointUri}/{id.Substring(0, 24)}");

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                auctionServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
            }

            [Theory, AutoData]
            public async Task IdValido_UsuarioNaoEncontrado_RetornaNotFound(string id)
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{endpointUri}/{id.Substring(0,24)}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                auctionServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task IdValido_UsuarioEncontrado_RetornaOk(string id, Auction auction)
            {
                auctionServiceMock.Setup(mock => mock.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(auction);

                HttpResponseMessage response = await httpClient.GetAsync($"{endpointUri}/{id.Substring(0, 24)}");
                bool wasDeserializationSuccessful = response.Content.TryGetContentValue(out AuctionDto auctionDtoReturned);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(wasDeserializationSuccessful);
                Assert.NotNull(auctionDtoReturned);
                auctionServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
            }
        }

        public class Create : AuctionControllerTest
        {
            public Create(RestApiWebApplicationFactory restApiWebApplicationFactory) : base(restApiWebApplicationFactory) { }

            [Fact]
            public async Task PayloadVazio_RetornaBadRequest()
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(endpointUri, (CreateAuctionInputDto)null);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                userServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Never);
                auctionServiceMock.Verify(mock => mock.CreateAsync(It.IsAny<Auction>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task PayloadPreenchido_UsuarioNaoEncontrado_RetornaBadRequest(CreateAuctionInputDto createAuctionInputDto)
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(endpointUri, createAuctionInputDto);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                userServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
                auctionServiceMock.Verify(mock => mock.CreateAsync(It.IsAny<Auction>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task PayloadPreenchido_UsuarioEncontrado_RetornaCreated(CreateAuctionInputDto createAuctionInputDto, User user)
            {
                userServiceMock.Setup(mock => mock.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

                HttpResponseMessage response = await httpClient.PostAsJsonAsync(endpointUri, createAuctionInputDto);
                bool wasDeserializationSuccessful = response.Content.TryGetContentValue(out AuctionDto auctionDtoReturned);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                Assert.NotNull(response.Headers.Location);
                Assert.True(wasDeserializationSuccessful);
                Assert.NotNull(auctionDtoReturned);
                userServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
                auctionServiceMock.Verify(mock => mock.CreateAsync(It.IsAny<Auction>()), Times.Once);
            }
        }

        public class Update : AuctionControllerTest
        {
            public Update(RestApiWebApplicationFactory restApiWebApplicationFactory) : base(restApiWebApplicationFactory) { }

            [Theory, AutoData]
            public async Task PayloadVazio_RetornaBadRequest(string id)
            {
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"{endpointUri}/{id.Substring(0, 24)}", (CreateAuctionInputDto)null);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                auctionServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Never);
                userServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Never);
                auctionServiceMock.Verify(mock => mock.UpdateAsync(It.IsAny<Auction>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task PayloadPreenchido_LeilaoNaoEncontrado_RetornaNotFound(AuctionDto updateAuctionDto)
            {
                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"{endpointUri}/{updateAuctionDto.Id.Substring(0, 24)}", updateAuctionDto);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                auctionServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
                userServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Never);
                auctionServiceMock.Verify(mock => mock.UpdateAsync(It.IsAny<Auction>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task PayloadPreenchido_LeilaoEncontrado_UsuarioNaoEncontrado_RetornaNotFound(AuctionDto updateAuctionDto, Auction auction)
            {
                auctionServiceMock.Setup(mock => mock.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(auction);

                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"{endpointUri}/{updateAuctionDto.Id.Substring(0, 24)}", updateAuctionDto);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                auctionServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
                userServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
                auctionServiceMock.Verify(mock => mock.UpdateAsync(It.IsAny<Auction>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task PayloadPreenchido_LeilaoEncontrado_UsuarioEncontrado_RetornaOk(AuctionDto updateAuctionDto, Auction auction, User user)
            {
                auctionServiceMock.Setup(mock => mock.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(auction);
                userServiceMock.Setup(mock => mock.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
                auctionServiceMock.Setup(mock => mock.UpdateAsync(It.IsAny<Auction>())).ReturnsAsync(auction);

                HttpResponseMessage response = await httpClient.PutAsJsonAsync($"{endpointUri}/{updateAuctionDto.Id.Substring(0, 24)}", updateAuctionDto);
                bool wasDeserializationSuccessful = response.Content.TryGetContentValue(out AuctionDto auctionDtoReturned);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(wasDeserializationSuccessful);
                Assert.NotNull(auctionDtoReturned);
                auctionServiceMock.Verify(mock => mock.UpdateAsync(It.IsAny<Auction>()), Times.Once);
                userServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
                auctionServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
            }
        }

        public class Delete : AuctionControllerTest
        {
            public Delete(RestApiWebApplicationFactory restApiWebApplicationFactory) : base(restApiWebApplicationFactory) { }

            [Theory, AutoData]
            public async Task LeilaoNaoEncontrado_RetornaNotFound(string id)
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"{endpointUri}/{id.Substring(0, 24)}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                auctionServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
                auctionServiceMock.Verify(mock => mock.DeleteAsync(It.IsAny<string>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task LeilaoEncontrado_RetornaNoContent(string id, Auction auction)
            {
                auctionServiceMock.Setup(mock => mock.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(auction);
                
                HttpResponseMessage response = await httpClient.DeleteAsync($"{endpointUri}/{id.Substring(0, 24)}");

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                auctionServiceMock.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
                auctionServiceMock.Verify(mock => mock.DeleteAsync(It.IsAny<string>()), Times.Once);
            }
        }
    }
}
