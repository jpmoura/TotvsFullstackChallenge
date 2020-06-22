using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Test.Dtos.Auctions
{
    public class AuctionDtoTest
    {
        public class From : AuctionDtoTest
        {
            [Fact]
            public void EntidadeNula_RetornaNull()
            {
                AuctionDto result = AuctionDto.From(null);

                Assert.Null(result);
            }

            [Theory, AutoMoqData]
            public void EntidadePreenchida_RetornaDtoPreenchido(Auction auction)
            {
                AuctionDto result = AuctionDto.From(auction);

                Assert.Equal(auction.Close, result.Close);
                Assert.Equal(auction.Id, result.Id);
                Assert.Equal(auction.InitialBid, result.InitialBid);
                Assert.Equal(auction.IsUsed, result.IsUsed);
                Assert.Equal(auction.Name, result.Name);
                Assert.Equal(auction.Open, result.Open);
                Assert.Equal(auction.Responsible.Id, result.Responsible.Id);
                Assert.Equal(auction.Responsible.Name, result.Responsible.Name);
            }
        }
        
        public class To : AuctionDtoTest
        {
            [Theory, AutoMoqData]
            public void RetornaEntidade(AuctionDto auctionDto, User user)
            {
                Auction result = auctionDto.To(user);

                Assert.Equal(auctionDto.Close, result.Close);
                Assert.Equal(auctionDto.Id, result.Id);
                Assert.Equal(auctionDto.InitialBid, result.InitialBid);
                Assert.Equal(auctionDto.IsUsed, result.IsUsed);
                Assert.Equal(auctionDto.Name, result.Name);
                Assert.Equal(auctionDto.Open, result.Open);
                Assert.Equal(user.Id, result.Responsible.Id);
                Assert.Equal(user.Name, result.Responsible.Name);
            }
        }
    }
}
