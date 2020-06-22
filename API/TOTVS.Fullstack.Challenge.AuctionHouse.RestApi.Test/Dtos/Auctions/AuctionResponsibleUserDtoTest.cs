using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Test.Dtos.Auctions
{
    public class AuctionResponsibleUserDtoTest
    {
        public class From : AuctionResponsibleUserDtoTest
        {
            [Fact]
            public void EntidadeNula_RetornaNull()
            {
                AuctionResponsibleUserDto result = AuctionResponsibleUserDto.From(null);

                Assert.Null(result);
            }

            [Theory, AutoMoqData]
            public void EntidadePreenchida_RetornaDtoPreenchido(AuctionResponsibleUser user)
            {
                AuctionResponsibleUserDto result = AuctionResponsibleUserDto.From(user);

                Assert.Equal(user.Id, result.Id);
                Assert.Equal(user.Name, result.Name);
            }
        }
    }
}
