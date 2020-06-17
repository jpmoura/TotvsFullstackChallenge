using AutoFixture.Xunit2;
using Moq;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Test.Security
{
    public class BaseAuthenticationServiceTest
    {
        public class AuthenticateAsync
        {
            [Theory]
            [InlineAutoMoqData(null)]
            [InlineAutoMoqData("")]
            [InlineAutoMoqData(" ")]
            public async Task UsernameInvalido_LancaInvalidParameterException(string username, string password, [Frozen] Mock<IUserService> userService, BaseAuthenticationService sut)
            {
                await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AuthenticateAsync(username, password));
                userService.Verify(mock => mock.GetByUsernameAsync(It.IsAny<string>()), Times.Never);
            }

            [Theory]
            [InlineAutoMoqData(null)]
            [InlineAutoMoqData("")]
            [InlineAutoMoqData(" ")]
            public async Task UsernameValido_SenhaInvalida_LancaInvalidParameterException(string password, string username, [Frozen] Mock<IUserService> userService, BaseAuthenticationService sut)
            {
                await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AuthenticateAsync(username, password));
                userService.Verify(mock => mock.GetByUsernameAsync(It.IsAny<string>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task UsernameValido_SenhaValida_UsuarioNaoEncontrado_LancaInvalidParameterException(string username, string password, [Frozen] Mock<IUserService> userService, BaseAuthenticationService sut)
            {
                await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AuthenticateAsync(username, password));

                userService.Verify(mock => mock.GetByUsernameAsync(It.IsAny<string>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task UsernameValido_SenhaValida_UsuarioEncontrado_UsuarioDesativado_LancaInvalidParameterException(string username, string password, User user, [Frozen] Mock<IUserService> userService, BaseAuthenticationService sut)
            {
                user.IsActive = false;

                userService.Setup(mock => mock.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(user);

                await Assert.ThrowsAsync<DisabledUserException>(() => sut.AuthenticateAsync(username, password));

                userService.Verify(mock => mock.GetByUsernameAsync(It.IsAny<string>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task UsernameValido_SenhaValida_UsuarioEncontrado_UsuarioAtivado_SenhaErrada_LancaInvalidParameterException(string username, string password, User user, [Frozen] Mock<IUserService> userService, BaseAuthenticationService sut)
            {
                userService.Setup(mock => mock.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(user);

                await Assert.ThrowsAsync<InvalidParameterException>(() => sut.AuthenticateAsync(username, password));

                userService.Verify(mock => mock.GetByUsernameAsync(It.IsAny<string>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task UsernameValido_SenhaValida_UsuarioEncontrado_UsuarioAtivado_SenhaCorreta_LancaInvalidParameterException(string username, string password, User user, [Frozen] Mock<IUserService> userService, BaseAuthenticationService sut)
            {
                user.Password = password;
                userService.Setup(mock => mock.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(user);

                User returnedUser = await sut.AuthenticateAsync(username, password);

                userService.Verify(mock => mock.GetByUsernameAsync(It.IsAny<string>()), Times.Once);
                Assert.Equal(user, returnedUser);
            }
        }
    }
}
