using AutoFixture.Xunit2;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Repositories.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Test.Core
{
    public class UserServiceTest
    {
        public class GetByIdAsync
        {
            [Theory]
            [InlineAutoMoqData(null)]
            [InlineAutoMoqData("")]
            [InlineAutoMoqData(" ")]
            public async Task IdInvalido_LancaInvalidParameterException(string id, [Frozen] Mock<IUserRepository> userRepository, UserService sut)
            {
                await Assert.ThrowsAsync<InvalidParameterException>(() => sut.GetByIdAsync(id));
                userRepository.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task IdValido_UsuarioNaoEncontrado_RetornaNull(string id, [Frozen] Mock<IUserRepository> userRepository, UserService sut)
            {
                userRepository.Setup(mock => mock.GetByIdAsync(It.IsAny<string>())).ReturnsAsync((User)null);

                User userReturned = await sut.GetByIdAsync(id);

                Assert.Null(userReturned);
                userRepository.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task IdValido_UsuarioEncontrado_RetornaUsuario(string id, [Frozen] Mock<IUserRepository> userRepository, UserService sut)
            {
                User userReturned = await sut.GetByIdAsync(id);

                Assert.NotNull(userReturned);
                userRepository.Verify(mock => mock.GetByIdAsync(It.IsAny<string>()), Times.Once);
            }
        }

        public class GetByUsernameAsync
        {
            [Theory]
            [InlineAutoMoqData(null)]
            [InlineAutoMoqData("")]
            [InlineAutoMoqData(" ")]
            public async Task UsernameInvalido_LancaInvalidParameterException(string username, [Frozen] Mock<IUserRepository> userRepository, UserService sut)
            {
                await Assert.ThrowsAsync<InvalidParameterException>(() => sut.GetByIdAsync(username));
                userRepository.Verify(mock => mock.GetByUsernameAsync(It.IsAny<string>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public async Task UsernameValido_UsuarioNaoEncontrado_RetornaNull(string username, [Frozen] Mock<IUserRepository> userRepository, UserService sut)
            {
                userRepository.Setup(mock => mock.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User)null);

                User userReturned = await sut.GetByUsernameAsync(username);

                Assert.Null(userReturned);
                userRepository.Verify(mock => mock.GetByUsernameAsync(It.IsAny<string>()), Times.Once);
            }

            [Theory, AutoMoqData]
            public async Task UsernameValido_UsuarioEncontrado_RetornaUsuario(string username, [Frozen] Mock<IUserRepository> userRepository, UserService sut)
            {
                User userReturned = await sut.GetByUsernameAsync(username);
                
                Assert.NotNull(userReturned);
                userRepository.Verify(mock => mock.GetByUsernameAsync(It.IsAny<string>()), Times.Once);
            }
        }

        public class List
        {
            [Theory, AutoMoqData]
            public void PaginacaoInvalida_LancaValidationException(Pagination pagination, [Frozen] Mock<IUserRepository> userRepository, [Frozen] Mock<IValidator<Pagination>> paginationValidator, UserService sut)
            {
                paginationValidator.Setup(mock => mock.Validate(It.IsAny<ValidationContext>())).Throws(new ValidationException(string.Empty));

                Assert.Throws<ValidationException>(() => sut.List(pagination));
                paginationValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Once);
                userRepository.Verify(mock => mock.List(It.IsAny<Pagination>()), Times.Never);
            }

            [Theory, AutoMoqData]
            public void PaginacaoValida_RetornaColecaoQueryableLeilao(Pagination pagination, [Frozen] Mock<IUserRepository> userRepository, [Frozen] Mock<IValidator<Pagination>> paginationValidator, UserService sut)
            {
                paginationValidator.Setup(mock => mock.Validate(It.IsAny<ValidationContext>())).Returns(new ValidationResult());

                IQueryable<User> userCollectionReturned = sut.List(pagination);

                Assert.NotNull(userCollectionReturned);
                paginationValidator.Verify(mock => mock.Validate(It.IsAny<ValidationContext>()), Times.Once);
                userRepository.Verify(mock => mock.List(It.IsAny<Pagination>()), Times.Once);
            }
        }
    }
}
