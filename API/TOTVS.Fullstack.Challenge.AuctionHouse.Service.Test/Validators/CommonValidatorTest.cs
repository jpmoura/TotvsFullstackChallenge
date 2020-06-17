using AutoFixture.Xunit2;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validations;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Test.Validators
{
    public class CommonValidatorTest
    {
        public class EnforceResourceFound
        {
            [Theory]
            [InlineAutoData(null)]
            public void RecursoNaoEncontrado_LancaNotFoundException(object resource, string resourceName)
            {
                Assert.Throws<ResourceNotFoundException>(() => CommonValidator.EnforceResourceFound(resource, typeof(object), resourceName));

                Assert.Null(resource);
            }

            [Theory, AutoData]
            public void RecursoEncontrado_RetornaVoid(object resource, string resourceName)
            {
                CommonValidator.EnforceResourceFound(resource, typeof(object), resourceName);

                Assert.NotNull(resource);
            }
        }

        public class EnforceNotEmpty
        {
            [Theory]
            [InlineAutoData(null)]
            [InlineAutoData("")]
            [InlineAutoData(" ")]
            public void StringInvalida_LancaInvalidParameterException(string parameter, string parameterName)
            {
                Assert.Throws<InvalidParameterException>(() => CommonValidator.EnforceNotEmpty(parameter, parameterName));
            }

            [Theory, AutoData]
            public void StringValida_RetornaVoid(string parameter, string parameterName)
            {
                CommonValidator.EnforceNotEmpty(parameter, parameterName);

                Assert.True(true);
            }
        }

        public class EnforceNorNull
        {
            [Theory]
            [InlineAutoData(null)]
            public void ParametroNulo_LancaNotFoundException(object parameter, string parameterName)
            {
                Assert.Throws<InvalidParameterException>(() => CommonValidator.EnforceNotNull(parameter, parameterName));

                Assert.Null(parameter);
            }

            [Theory, AutoData]
            public void ParametroPreenchido_RetornaVoid(object parameter, string parameterName)
            {
                CommonValidator.EnforceNotNull(parameter, parameterName);

                Assert.NotNull(parameter);
            }
        }
    }
}
