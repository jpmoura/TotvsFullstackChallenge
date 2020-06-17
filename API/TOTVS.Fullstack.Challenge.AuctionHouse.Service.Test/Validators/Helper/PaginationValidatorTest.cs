using FluentValidation;
using FluentValidation.TestHelper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validators.Helper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Tests.Util.Attributes;
using Xunit;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Test.Validators.Helper
{
    public class PaginationValidatorTest
    {
        public class DefaultRuleSet
        {
            [Theory, AutoMoqData]
            public void PaginaNaoPreenchida_TamanhoPaginaNaoPreenchido_RetornaVoid(Pagination pagination, PaginationValidator sut)
            {
                pagination.Page = null;
                pagination.PageSize = null;

                sut.ValidateAndThrow(pagination);

                sut.ShouldNotHaveValidationErrorFor(pagination => pagination.Page, pagination);
                sut.ShouldNotHaveValidationErrorFor(pagination => pagination.PageSize, pagination);

            }

            [Theory, AutoMoqData]
            public void PaginaNaoPreenchida_TamanhoPaginaPreenchido_RetornaVoid(Pagination pagination, PaginationValidator sut)
            {
                pagination.Page = null;

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(pagination));
                sut.ShouldHaveValidationErrorFor(pagination => pagination.Page, pagination);
                sut.ShouldNotHaveValidationErrorFor(pagination => pagination.PageSize, pagination);

            }

            [Theory, AutoMoqData]
            public void PaginaPreenchida_TamanhoPaginaNaoPreenchido_RetornaVoid(Pagination pagination, PaginationValidator sut)
            {
                pagination.PageSize = null;

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(pagination));
                sut.ShouldHaveValidationErrorFor(pagination => pagination.PageSize, pagination);
                sut.ShouldHaveValidationErrorFor(pagination => pagination.Page, pagination);

            }

            [Theory]
            [InlineAutoMoqData(0)]
            [InlineAutoMoqData(int.MinValue)]
            public void PaginaPreenchida_PaginaInvalida_TamanhoPaginaPreenchido_TamanhoPaginaValido_LancaValidationException(int? page, Pagination pagination, PaginationValidator sut)
            {
                pagination.Page = page;

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(pagination));
                sut.ShouldHaveValidationErrorFor(pagination => pagination.Page, pagination);
                sut.ShouldNotHaveValidationErrorFor(pagination => pagination.PageSize, pagination);
            }

            [Theory]
            [InlineAutoMoqData(0)]
            [InlineAutoMoqData(int.MinValue)]
            public void PaginaPreenchida_PaginaValida_TamanhoPaginaPreenchido_TamanhoPaginaInvalido_LancaValidationException(int? pageSize, Pagination pagination, PaginationValidator sut)
            {
                pagination.PageSize = pageSize;

                Assert.Throws<ValidationException>(() => sut.ValidateAndThrow(pagination));
                sut.ShouldHaveValidationErrorFor(pagination => pagination.PageSize, pagination);
                sut.ShouldNotHaveValidationErrorFor(pagination => pagination.Page, pagination);
            }

            [Theory, AutoMoqData]
            public void PaginaPreenchida_PaginaValida_TamanhoPaginaPreenchido_TamanhoPaginaValido_RetornaVoid(Pagination pagination, PaginationValidator sut)
            {
                sut.ValidateAndThrow(pagination);

                sut.ShouldNotHaveValidationErrorFor(pagination => pagination.Page, pagination);
                sut.ShouldNotHaveValidationErrorFor(pagination => pagination.PageSize, pagination);
            }
        }
    }
}
