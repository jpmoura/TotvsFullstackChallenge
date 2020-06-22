using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Test.Dtos.Core
{
    public class UserDtoTest
    {
        public class From : UserDtoTest
        {
            [Fact]
            public void EntidadeNula_RetornaNull()
            {
                UserDto result = UserDto.From(null);

                Assert.Null(result);
            }

            [Theory, AutoMoqData]
            public void EntidadePreenchida_RetornaDtoPreenchido(User user)
            {
                UserDto result = UserDto.From(user);

                Assert.Equal(user.Id, result.Id);
                Assert.Equal(user.Name, result.Name);
            }
        }
    }
}
