using AutoFixture.Xunit2;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Repositories;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Auctions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Test.Auctions
{
    public class AuctionServiceTest
    {
        public class CreateAsync
        {
            [Theory, AutoMoqData]
            public async Task LeilaoNulo_LancaInvalidParameterException([Frozen] Mock<IRepository<Auction>> auctionRepository, [Frozen]Mock<IValidator<Auction>> auctionValidator, AuctionService sut)
            {
                await Assert.ThrowsAsync<InvalidParameterException>(() => sut.CreateAsync(null));
                auctionValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Never);
                auctionRepository.Verify(mock => mock.CreateAsync(It.IsAny<Auction>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task LeilaoPreenchido_LeilaoInvalido_LancaValidationException(Auction auction, [Frozen] Mock<IRepository<Auction>> auctionRepository, [Frozen]Mock<IValidator<Auction>> auctionValidator, AuctionService sut)
            {
                auctionValidator.Setup(mock => mock.Validate(It.IsAny<ValidationContext>())).Throws(new ValidationException(string.Empty));

                await Assert.ThrowsAsync<ValidationException>(() => sut.CreateAsync(auction));
                auctionValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Once);
                auctionRepository.Verify(mock => mock.CreateAsync(It.IsAny<Auction>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task LeilaoPreenchido_LeilaoValido_RetornaVoid(Auction auction, [Frozen] Mock<IRepository<Auction>> auctionRepository, [Frozen]Mock<IValidator<Auction>> auctionValidator, AuctionService sut)
            {
                auctionValidator.Setup(mock => mock.Validate(It.IsAny<ValidationContext>())).Returns(new ValidationResult());

                await sut.CreateAsync(auction);

                auctionValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Once);
                auctionRepository.Verify(mock => mock.CreateAsync(It.IsAny<Auction>()), Times.Once);
            }
        }

        public class DeleteAsync
        {
            [Theory]
            [InlineAutoMoqData(null)]
            [InlineAutoMoqData("")]
            [InlineAutoMoqData(" ")]
            public async Task IdInvalido_LancaInvalidParameterException(string id, [Frozen] Mock<IRepository<Auction>> auctionRepository, AuctionService sut)
            {
                await Assert.ThrowsAsync<InvalidParameterException>(() => sut.DeleteAsync(id));
                auctionRepository.Verify(mock => mock.DeleteAsync(It.IsAny<string>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task IdValido_RetornaVoid(string id, [Frozen] Mock<IRepository<Auction>> auctionRepository, AuctionService sut)
            {
                await sut.DeleteAsync(id);
                auctionRepository.Verify(mock => mock.DeleteAsync(It.IsAny<string>()), Times.Once);
            }
        }

        public class GetByIdAsync
        {
            [Theory]
            [InlineAutoMoqData(null)]
            [InlineAutoMoqData("")]
            [InlineAutoMoqData(" ")]
            public async Task IdInvalido_LancaInvalidParameterException(string id, [Frozen] Mock<IRepository<Auction>> auctionRepository, AuctionService sut)
            {
                await Assert.ThrowsAsync<InvalidParameterException>(() => sut.DeleteAsync(id));
                auctionRepository.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task IdValido_LeilaoNaoEncontrado_RetornaLeilao(string id, [Frozen] Mock<IRepository<Auction>> auctionRepository, AuctionService sut)
            {
                auctionRepository.Setup(mock => mock.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((Auction)null);

                Auction auctionReturned = await sut.GetByIdAsync(id);

                Assert.Null(auctionReturned);
                auctionRepository.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task IdValido_LeilaoEncontrado_RetornaLeilao(string id, [Frozen] Mock<IRepository<Auction>> auctionRepository, AuctionService sut)
            {
                Auction auctionReturned = await sut.GetByIdAsync(id);

                Assert.NotNull(auctionReturned);
                auctionRepository.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
            }
        }

        public class List
        {
            [Theory, AutoMoqData]
            public void PaginacaoInvalida_LancaValidationException(Pagination pagination, [Frozen] Mock<IRepository<Auction>> auctionRepository, [Frozen] Mock<IValidator<Pagination>> paginationValidator, AuctionService sut)
            {
                paginationValidator.Setup(mock => mock.Validate(It.IsAny<ValidationContext>())).Throws(new ValidationException(string.Empty));
                
                Assert.Throws<ValidationException>(() => sut.List(pagination));
                paginationValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Once);
                auctionRepository.Verify(mock => mock.List(It.IsAny<Pagination>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public void PaginacaoValida_RetornaColecaoQueryableLeilao(Pagination pagination, [Frozen] Mock<IRepository<Auction>> auctionRepository, [Frozen] Mock<IValidator<Pagination>> paginationValidator, AuctionService sut)
            {
                paginationValidator.Setup(mock => mock.Validate(It.IsAny<ValidationContext>())).Returns(new ValidationResult());

                IQueryable<Auction> auctionCollectionReturned = sut.List(pagination);

                Assert.NotNull(auctionCollectionReturned);
                paginationValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Once);
                auctionRepository.Verify(mock => mock.List(It.IsAny<Pagination>()), Times.Once);
            }
        }

        public class UpdateAsync
        {
            [Theory, AutoMoqData]
            public async Task LeilaoNulo_LancaInvalidParameterException([Frozen] Mock<IRepository<Auction>> auctionRepository, [Frozen]Mock<IValidator<Auction>> auctionValidator, AuctionService sut)
            {
                await Assert.ThrowsAsync<InvalidParameterException>(() => sut.UpdateAsync(null));
                auctionValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Never);
                auctionRepository.Verify(mock => mock.UpdateAsync(It.IsAny<Auction>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task LeilaoPreenchido_LeilaoInvalido_LancaValidationException(Auction auction, [Frozen] Mock<IRepository<Auction>> auctionRepository, [Frozen]Mock<IValidator<Auction>> auctionValidator, AuctionService sut)
            {
                auctionValidator.Setup(mock => mock.Validate(It.IsAny<ValidationContext>())).Throws(new ValidationException(string.Empty));

                await Assert.ThrowsAsync<ValidationException>(() => sut.CreateAsync(auction));
                auctionValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Once);
                auctionRepository.Verify(mock => mock.UpdateAsync(It.IsAny<Auction>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task LeilaoPreenchido_LeilaoValido_RetornaLeilaoAtualizado(Auction auction, [Frozen] Mock<IRepository<Auction>> auctionRepository, [Frozen]Mock<IValidator<Auction>> auctionValidator, AuctionService sut)
            {
                auctionValidator.Setup(mock => mock.Validate(It.IsAny<ValidationContext>())).Returns(new ValidationResult());

                Auction updatedAuctionReturned = await sut.UpdateAsync(auction);

                Assert.NotNull(updatedAuctionReturned);
                auctionValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Once);
                auctionRepository.Verify(mock => mock.UpdateAsync(It.IsAny<Auction>()), Times.Once);
            }
        }
    }
}
