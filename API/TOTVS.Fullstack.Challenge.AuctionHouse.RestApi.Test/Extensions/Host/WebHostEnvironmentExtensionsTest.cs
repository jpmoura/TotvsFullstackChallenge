using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Extensions.Host;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Test.Extensions.Host
{
    public class WebHostEnvironmentExtensionsTest
    {
        public class IsTesting : WebHostEnvironmentExtensionsTest
        {
            [Theory]
            [InlineAutoMoqData("testing")]
            [InlineAutoMoqData("TESTING")]
            [InlineAutoMoqData("TeStInG")]
            public void AmbienteDeTeste_RetornaTrue(string environmentName, IWebHostEnvironment webHostEnvironment)
            {
                webHostEnvironment.EnvironmentName = environmentName;

                bool result = webHostEnvironment.IsTesting();

                Assert.True(result);
            }

            [Theory, AutoMoqData]
            public void AmbienteDiferenteDeTeste_RetornaFalse(string environmentName, IWebHostEnvironment webHostEnvironment)
            {
                webHostEnvironment.EnvironmentName = environmentName;

                bool result = webHostEnvironment.IsTesting();

                Assert.False(result);
            }
        }
    }
}
