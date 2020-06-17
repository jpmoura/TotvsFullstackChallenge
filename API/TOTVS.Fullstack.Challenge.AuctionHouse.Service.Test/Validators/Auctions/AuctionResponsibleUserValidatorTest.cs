using FluentValidation;
using FluentValidation.TestHelper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validators.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Test.Validators.Auctions
{
    public class AuctionResponsibleUserValidatorTest
    {
        public class DefaultRuleSet
        {
            [Theory]
            [InlineAutoMoqData(null)]
            [InlineAutoMoqData("")]
            [InlineAutoMoqData(" ")]
            public void IdInvalido_LancaValidationException(string id, AuctionResponsibleUser auctionResponsibleUser, AuctionResponsibleUserValidator sut)
            {
                auctionResponsibleUser.Id = id;

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(auctionResponsibleUser));
                sut.ShouldNotHaveValidationErrorFor(auctionResponsibleUser => auctionResponsibleUser.Name, auctionResponsibleUser);
            }

            [Theory]
            [InlineAutoMoqData(null)]
            [InlineAutoMoqData("")]
            [InlineAutoMoqData(" ")]
            public void IdValido_NomeInvalido_LancaValidationException(string name, AuctionResponsibleUser auctionResponsibleUser, AuctionResponsibleUserValidator sut)
            {
                auctionResponsibleUser.Name = name;

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(auctionResponsibleUser));
                sut.ShouldNotHaveValidationErrorFor(auctionResponsibleUser => auctionResponsibleUser.Id, auctionResponsibleUser);
            }

            [Theory, AutoMoqData]
            public void IdValido_NomeValido_RetornaVoid(AuctionResponsibleUser auctionResponsibleUser, AuctionResponsibleUserValidator sut)
            {
                sut.ValidateAndThrow(auctionResponsibleUser);

                sut.ShouldNotHaveValidationErrorFor(auctionResponsibleUser => auctionResponsibleUser.Id, auctionResponsibleUser);
                sut.ShouldNotHaveValidationErrorFor(auctionResponsibleUser => auctionResponsibleUser.Name, auctionResponsibleUser);
            }
        }
    }
}
