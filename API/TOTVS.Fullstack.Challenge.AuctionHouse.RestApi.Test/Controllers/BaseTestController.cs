using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Factories;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Test.Controllers
{
    public class BaseTestController : IClassFixture<RestApiWebApplicationFactory>
    {
        /// <summary>
        /// Cliente HTTP utlizado para realizar as chamadas
        /// </summary>
        protected readonly HttpClient httpClient;

        /// <summary>
        /// URI do endpoint a ser testado
        /// </summary>
        protected string endpointUri;

        #region Mocks

        /// <summary>
        /// Mock do serviço de leilão
        /// </summary>
        protected readonly Mock<IAuctionService> auctionServiceMock;

        /// <summary>
        /// Mock do serviço de usuário
        /// </summary>
        protected readonly Mock<IUserService> userServiceMock;

        /// <summary>
        /// Mock de serviço de autenticação com JWT
        /// </summary>
        protected readonly Mock<IJwtAuthenticationService> jwtAuthenticationServiceMock;

        #endregion

        /// <summary>
        /// Construtor
        /// </summary>
        public BaseTestController(RestApiWebApplicationFactory webApplicationFactory)
        {
            // Criação das instâncias de mocks de serviços
            IFixture fixture = new Fixture().Customize(new AutoMoqCustomization());
            auctionServiceMock = fixture.Create<Mock<IAuctionService>>();
            jwtAuthenticationServiceMock = fixture.Create<Mock<IJwtAuthenticationService>>();
            userServiceMock = fixture.Create<Mock<IUserService>>();

            httpClient = webApplicationFactory.WithWebHostBuilder(ConfigureTestWebHost).CreateClient();
        }

        /// <summary>
        /// Substitui um serviço por uma instância de mock
        /// </summary>
        /// <typeparam name="T">Tipo do serviço a ser substituído</typeparam>
        /// <param name="serviceCollection">Coleção de serviços da aplicação</param>
        /// <param name="originalService">Descritor do serviço original</param>
        /// <param name="mockService">Mock do serviço original</param>
        private void SwapService<T>(IServiceCollection serviceCollection, ServiceDescriptor originalService, Mock<T> mockService) where T : class
        {
            serviceCollection.Remove(originalService);
            serviceCollection.Add(new ServiceDescriptor(originalService.ServiceType, mockService.Object));
        }

        /// <summary>
        /// Substitui os serviços originais por instâncias "mockadas"
        /// </summary>
        /// <param name="serviceCollection">Coleção de serviços</param>
        private void SwapOriginalServicesWithMock(IServiceCollection serviceCollection)
        {
            // Substituição do serviço de leilão pelo seu mock
            ServiceDescriptor descriptor = new ServiceDescriptor(typeof(IAuctionService), typeof(AuctionService), ServiceLifetime.Singleton);
            SwapService(serviceCollection, descriptor, auctionServiceMock);

            // Substituição do serviço de autenticação via JWT pelo seu mock
            descriptor = new ServiceDescriptor(typeof(IJwtAuthenticationService), typeof(JwtAuthenticationService), ServiceLifetime.Singleton);
            SwapService(serviceCollection, descriptor, jwtAuthenticationServiceMock);

            // Substituição do serviço de usuário pelo seu mock
            descriptor = new ServiceDescriptor(typeof(IUserService), typeof(UserService), ServiceLifetime.Singleton);
            SwapService(serviceCollection, descriptor, userServiceMock);
        }

        /// <summary>
        /// Configura o Web Host que responderá as requisições nos testes de integração
        /// </summary>
        /// <param name="webHostBuilder">Construtor do web host</param>
        private void ConfigureTestWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseEnvironment(Domain.Constants.EnvironmentName.Testing);

            webHostBuilder.ConfigureTestServices(SwapOriginalServicesWithMock);
        }
    }
}
