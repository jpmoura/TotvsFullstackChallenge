using AutoFixture.Xunit2;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
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
            public void IdPreenchido_LancaValidationException(string id, Auction auction, [Frozen] Mock<IValidator<AuctionResponsibleUser>> auctionResponsibleUserValidator, AuctionValidator sut)
            {
                auction.Close = auction.Open.AddDays(1);
                auction.Id = id;

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(auction, ValidationRuleSetName.Create));

                sut.ShouldHaveValidationErrorFor(auction => auction.Id, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Name, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.InitialBid, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.IsUsed, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Open, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Close, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Responsible, auction, ValidationRuleSetName.Create);
                auctionResponsibleUserValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Never);
            }

            [Theory]
            [InlineAutoMoqData(null)]
            public void IdNulo_RetornaVoid(string id, Auction auction, [Frozen] Mock<IValidator<AuctionResponsibleUser>> auctionResponsibleUserValidator, AuctionValidator sut)
            {
                auction.Close = auction.Open.AddDays(1);
                auction.Id = id;

                sut.ValidateAndThrow(auction, ValidationRuleSetName.Create);

                sut.ShouldNotHaveValidationErrorFor(auction => auction.Id, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Name, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.InitialBid, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.IsUsed, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Open, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Close, auction, ValidationRuleSetName.Create);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Responsible, auction, ValidationRuleSetName.Create);
                auctionResponsibleUserValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Never);
            }
        }

        public class DefaultRuleSet
        {
            [Theory]
            [InlineAutoMoqData(double.MinValue)]
            public void ValorInicialInvalido_LancaValidationException(double initialBid, Auction auction, [Frozen] Mock<IValidator<AuctionResponsibleUser>> auctionResponsibleUserValidator, AuctionValidator sut)
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
                auctionResponsibleUserValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.AtLeastOnce);
            }

            [Theory]
            [InlineAutoMoqData(null)]
            [InlineAutoMoqData("")]
            [InlineAutoMoqData(" ")]
            public void ValorInicialValido_NomeInvalido_LancaValidationException(string name, Auction auction, [Frozen] Mock<IValidator<AuctionResponsibleUser>> auctionResponsibleUserValidator, AuctionValidator sut)
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
                auctionResponsibleUserValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.AtLeastOnce);
            }

            [Theory, AutoMoqData]
            public void ValorInicialValido_NomeInvalido_DataAberturaInvalida_LancaValidationException(Auction auction, [Frozen] Mock<IValidator<AuctionResponsibleUser>> auctionResponsibleUserValidator, AuctionValidator sut)
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
                auctionResponsibleUserValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.AtLeastOnce);
            }

            [Theory, AutoMoqData]
            public void ValorInicialValido_NomeInvalido_DataAberturaValida_DataFechamentoInvalida_LancaValidationException(Auction auction, [Frozen] Mock<IValidator<AuctionResponsibleUser>> auctionResponsibleUserValidator, AuctionValidator sut)
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
                auctionResponsibleUserValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.AtLeastOnce);
            }

            [Theory, AutoMoqData]
            public void ValorInicialValido_NomeInvalido_DataAberturaValida_DataFechamentoValida_UsuarioReponsavelInvalido_LancaValidationException(Auction auction, [Frozen] Mock<IValidator<AuctionResponsibleUser>> auctionResponsibleUserValidator, AuctionValidator sut)
            {
                auction.Close = auction.Open.AddMilliseconds(1);
                auction.Responsible = null;

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(auction));
                sut.ShouldHaveValidationErrorFor(auction => auction.Responsible, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Open, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Id, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.IsUsed, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.InitialBid, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Name, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Close, auction);
                auctionResponsibleUserValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public void ValorInicialValido_NomeInvalido_DataAberturaValida_DataFechamentoValida_UsuarioReponsavelValido_RertornaVoid(Auction auction, [Frozen] Mock<IValidator<AuctionResponsibleUser>> auctionResponsibleUserValidator, AuctionValidator sut)
            {
                auction.Close = auction.Open.AddMilliseconds(1);

                sut.ValidateAndThrow(auction);
                
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Responsible, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Open, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Id, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.IsUsed, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.InitialBid, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Name, auction);
                sut.ShouldNotHaveValidationErrorFor(auction => auction.Close, auction);
                auctionResponsibleUserValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.AtLeastOnce);
            }
        }
    }
}
