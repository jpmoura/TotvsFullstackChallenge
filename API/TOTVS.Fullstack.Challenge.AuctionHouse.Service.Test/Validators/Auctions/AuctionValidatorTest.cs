using FluentValidation;
using FluentValidation.TestHelper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Constants;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validators.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Test.Validators.Auctions
{
    public class AuctionValidatorTest
    {
        public class CreateRuleSet
        {
            [Theory]
            [InlineAutoMoqData("")]
            [InlineAutoMoqData(" ")]
            [AutoMoqData]
            public void IdPreenchido_LancaValidationException(string id, Auction auction, AuctionValidator sut)
            {
                auction.Close = auction.Open.AddDays(1);
                auction.Id = id;

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(auction, ValidationRuleSetName.Create));

                sut.ShouldHaveValidationErrorFor(auction => auction.Id, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Name, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.InitialBid, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.IsUsed, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Open, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Close, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Responsible, auction);
            }

            [Theory]
            [InlineAutoMoqData(null)]
            public void IdNulo_RetornaVoid(string id, Auction auction, AuctionValidator sut)
            {
                auction.Close = auction.Open.AddDays(1);
                auction.Id = id;

                sut.ValidateAndThrow(auction, ValidationRuleSetName.Create);

                sut.ShouldNotHaveValidationErrorFor(auction => auction.Id, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Name, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.InitialBid, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.IsUsed, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Open, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Close, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Responsible, auction);
            }
        }

        public class DefaultRuleSet
        {
            [Theory]
            [InlineAutoMoqData(double.MinValue)]
            public void ValorInicialInvalido_LancaValidationException(double initialBid, Auction auction, AuctionValidator sut)
            {
                auction.InitialBid = initialBid;
                auction.Close = auction.Open.AddDays(1);

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(auction));
                sut.ShouldHaveValidationErrorFor(auction => auction.InitialBid, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Id, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.IsUsed, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Name, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Open, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Close, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Responsible, auction);
            }

            [Theory]
            [InlineAutoMoqData(null)]
            [InlineAutoMoqData("")]
            [InlineAutoMoqData(" ")]
            public void ValorInicialValido_NomeInvalido_LancaValidationException(string name, Auction auction, AuctionValidator sut)
            {
                auction.Name = name;
                auction.Close = auction.Open.AddDays(1);

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(auction));
                sut.ShouldHaveValidationErrorFor(auction => auction.Name, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Id, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.IsUsed, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.InitialBid, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Open, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Close, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Responsible, auction);
            }

            [Theory, AutoMoqData]
            public void ValorInicialValido_NomeInvalido_DataAberturaInvalida_LancaValidationException(Auction auction, AuctionValidator sut)
            {
                auction.Open = auction.Close.AddMilliseconds(1);

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(auction));
                sut.ShouldHaveValidationErrorFor(auction => auction.Open, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Id, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.IsUsed, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.InitialBid, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Name, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Close, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Responsible, auction);
            }

            [Theory, AutoMoqData]
            public void ValorInicialValido_NomeInvalido_DataAberturaValida_DataFechamentoInvalida_LancaValidationException(Auction auction, AuctionValidator sut)
            {
                auction.Close = auction.Open.AddMilliseconds(-1);

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(auction));
                sut.ShouldHaveValidationErrorFor(auction => auction.Open, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Id, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.IsUsed, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.InitialBid, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Name, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Close, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Responsible, auction);
            }
        }
    }
}
