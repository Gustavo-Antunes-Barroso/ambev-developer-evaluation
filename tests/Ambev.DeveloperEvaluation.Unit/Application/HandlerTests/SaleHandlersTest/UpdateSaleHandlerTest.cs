using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Unit.Application.Base;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Commands;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Entities;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.HandlerTests.SaleHandlersTest
{
    public class UpdateSaleHandlerTest : TestBase
    {
        private readonly UpdateSaleHandler _handler;
        private UpdateSaleCommand command;
        private Sale sale;

        public UpdateSaleHandlerTest()
        {
            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(true);
            _saleProductRepository.DeleteBySaleIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(true);
            _saleProductRepository.CreateManyAsync(Arg.Any<SaleProduct[]>(), Arg.Any<CancellationToken>()).Returns(new List<SaleProduct>().ToArray());
            _saleRepository.MongoDbUpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(true);

            _handler = new UpdateSaleHandler(_saleRepository, _saleProductRepository, _validateUpsertSaleService, _mapper);

            command = UpdateSaleCommandData.GenerateValidRandomUpdateSaleCommand();
            sale = SaleEntityData.GenerateRandomSaleData();
        }

        [Fact]
        public async Task Handler_ValidCommand_ReturnSuccess_Async()
        {
            _validateUpsertSaleService.ValidateUpsertSaleAsync(Arg.Any<UpsertSaleCommand>(), Arg.Any<CancellationToken>()).Returns(sale);

            var result = await _handler.Handle(command, new CancellationToken(false));

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.IsType<Guid>(result.Id);
        }

        [Fact]
        public async Task Handler_ValidCommand_ReturnsInvalidOperationException_Async()
        {
            _validateUpsertSaleService.ValidateUpsertSaleAsync(Arg.Any<UpsertSaleCommand>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromException<Sale>(new InvalidOperationException($"Sale total amount {command.TotalAmount} is wrong!")));

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _handler.Handle(command, new CancellationToken(false)));
        }
    }
}
