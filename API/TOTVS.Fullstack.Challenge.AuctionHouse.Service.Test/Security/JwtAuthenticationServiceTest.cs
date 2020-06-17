using AutoFixture.Xunit2;
using Moq;
using System;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Test.Security
{
    public class JwtAuthenticationServiceTest
    {
        public class AuthenticateAsync
        {
            [Theory, AutoMoqData]
            public async Task UsuarioInvalido_LancaExcecao(string username, string password, Exception exception, [Frozen]Mock<IUserService> userService, JwtAuthenticationService sut)
            {
                userService.Setup(mock => mock.GetByUsernameAsync(It.IsAny<string>())).ThrowsAsync(exception);

                await Assert.ThrowsAnyAsync<Exception>(() => sut.AuthenticateAsync(username, password));
            }

            [Theory, AutoMoqData]
            public async Task UsuarioValido_RetornaJwtAuthenticationResult(string username, string password, User user, [Frozen]Mock<IUserService> userService, JwtAuthenticationService sut)
            {
                DateTime minimumExpirationDateTime = DateTime.Now.AddDays(1);
                user.Password = password;

                userService.Setup(mock => mock.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(user);

                JwtAuthenticationResult jwtAuthenticationResultReturned = await sut.AuthenticateAsync(username, password);

                Assert.Equal(user.Id, jwtAuthenticationResultReturned.UserId);
                Assert.Equal(user.Name, jwtAuthenticationResultReturned.Name);
                Assert.Equal(user.Username, jwtAuthenticationResultReturned.Username);
                Assert.NotNull(jwtAuthenticationResultReturned.Token);
                Assert.True(jwtAuthenticationResultReturned.Expires >= minimumExpirationDateTime);
            }
        }
    }
}
