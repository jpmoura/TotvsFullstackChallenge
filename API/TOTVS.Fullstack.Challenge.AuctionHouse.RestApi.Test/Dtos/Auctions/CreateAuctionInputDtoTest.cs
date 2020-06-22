using Microsoft.AspNetCore.Hosting;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Test.Dtos.Auctions
{
    public class CreateAuctionInputDtoTest
    {
        public class To : CreateAuctionInputDto
        {
            [Theory, AutoMoqData]
            public void AmbienteDeTeste_RetornaIdPreenchido(User user, IWebHostEnvironment webHostEnvironment, CreateAuctionInputDto createAuctionInputDto)
            {
                webHostEnvironment.EnvironmentName = Domain.Constants.EnvironmentName.Testing;

                Auction result = createAuctionInputDto.To(user, webHostEnvironment);

                Assert.NotNull(result.Id);
                Assert.Equal(createAuctionInputDto.Close, result.Close);
                Assert.Equal(createAuctionInputDto.InitialBid, result.InitialBid);
                Assert.Equal(createAuctionInputDto.IsUsed, result.IsUsed);
                Assert.Equal(createAuctionInputDto.Name, result.Name);
                Assert.Equal(createAuctionInputDto.Open, result.Open);
                Assert.Equal(user.Id, result.Responsible.Id);
                Assert.Equal(user.Name, result.Responsible.Name);
            }

            [Theory, AutoMoqData]
            public void AmbienteDiferenteDeTeste_RetornaIdVazio(string environmentName, User user, IWebHostEnvironment webHostEnvironment, CreateAuctionInputDto createAuctionInputDto)
            {
                webHostEnvironment.EnvironmentName = environmentName;

                Auction result = createAuctionInputDto.To(user, webHostEnvironment);

                Assert.Null(result.Id);
                Assert.Equal(createAuctionInputDto.Close, result.Close);
                Assert.Equal(createAuctionInputDto.InitialBid, result.InitialBid);
                Assert.Equal(createAuctionInputDto.IsUsed, result.IsUsed);
                Assert.Equal(createAuctionInputDto.Name, result.Name);
                Assert.Equal(createAuctionInputDto.Open, result.Open);
                Assert.Equal(user.Id, result.Responsible.Id);
                Assert.Equal(user.Name, result.Responsible.Name);
            }
        }
    }
}
