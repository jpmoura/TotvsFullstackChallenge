using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Test.Dtos.Security
{
    public class JwtAuthenticationResultDtoTest
    {
        public class From : JwtAuthenticationResultDtoTest
        {
            [Fact]
            public void EntidadeNula_RetornaNull()
            {
                JwtAuthenticationResultDto result = JwtAuthenticationResultDto.From(null);

                Assert.Null(result);
            }

            [Theory, AutoMoqData]
            public void EntidadePreenchida_RetornaDtoPreenchido(JwtAuthenticationResult jwtAuthenticationResult)
            {
                JwtAuthenticationResultDto result = JwtAuthenticationResultDto.From(jwtAuthenticationResult);

                Assert.Equal(jwtAuthenticationResult.Expires, result.Expires);
                Assert.Equal(jwtAuthenticationResult.Name, result.Name);
                Assert.Equal(jwtAuthenticationResult.Token, result.Token);
                Assert.Equal(jwtAuthenticationResult.UserId, result.UserId);
                Assert.Equal(jwtAuthenticationResult.Username, result.Username);
            }
        }
    }
}
