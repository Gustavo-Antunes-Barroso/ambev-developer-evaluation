using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Unit.Application.Base;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Commands;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Services
{
    public class ValidateUpsertSaleServiceTest : TestBase
    {
        private readonly ValidateUpsertSaleService _service;
        private UpsertSaleCommand command;

        public ValidateUpsertSaleServiceTest()
        {
            _service = new(_userService, _subsidiaryService, _productService, _mapper);
            command = UpsertSaleCommandData.GenerateValidRandomUpsertSaleCommand();
        }

        [Fact]
        public async Task ValidateUpsertSale_ReturnsSuccess_Async()
        {
            _userService.GetAndValidateUser(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new User());
            _subsidiaryService.GetAndValidateSubsidiary(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new Subsidiary());
            _productService.GetAndValidateUpsertSaleProducts(Arg.Any<UpsertSaleCommand>(), Arg.Any<CancellationToken>()).Returns(new[] {new SaleProduct()});

            Sale sale = await _service.ValidateUpsertSaleAsync(command, cancellationToken);
            Assert.NotNull(sale);
        }


        [Fact]
        public async Task ValidateUpsertSale_ReturnsValidationException_Async()
        {
            await Assert.ThrowsAsync<ValidationException>(() => _service.ValidateUpsertSaleAsync(new UpsertSaleCommand(), cancellationToken));
        }
    }
}
