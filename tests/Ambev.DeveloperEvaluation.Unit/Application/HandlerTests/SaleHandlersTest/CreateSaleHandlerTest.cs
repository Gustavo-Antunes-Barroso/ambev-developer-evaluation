using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Unit.Application.Base;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Commands;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Entities;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.HandlerTests.SaleHandlersTest
{
    public class CreateSaleHandlerTest : TestBase
    {
        private readonly CreateSaleHandler _handler;

        private CreateSaleCommand command;
        private Sale sale;
        public CreateSaleHandlerTest()
        {
            sale = SaleEntityData.GenerateRandomSaleData();
            command = CreateSaleCommandData.GenerateValidRandomSaleCommand();

            _saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);
            _saleProductRepository.CreateManyAsync(Arg.Any<SaleProduct[]>(), Arg.Any<CancellationToken>()).Returns(new List<SaleProduct>().ToArray());
            _saleRepository.MongoDbCreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>()).Returns(sale);

            _handler = new CreateSaleHandler(_saleRepository, _saleProductRepository, _validateUpsertSaleService, _rabbitMQProducer, _mapper);
        }

        [Fact]
        public async Task Handler_ValidCommand_ReturnSuccess_Async()
        {
            _validateUpsertSaleService.ValidateUpsertSaleAsync(Arg.Any<UpsertSaleCommand>(), Arg.Any<CancellationToken>()).Returns(sale);

            var result = await _handler.Handle(command, cancellationToken);

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.IsType<Guid>(result.Id);
        }

        [Fact]
        public async Task Handler_ValidCommand_ReturnsInvalidOperationException_Async()
        {
            _validateUpsertSaleService.ValidateUpsertSaleAsync(Arg.Any<UpsertSaleCommand>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromException<Sale>(new InvalidOperationException($"Sale total amount with discount {command.TotalAmountWithDiscount} is wrong!")));

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _handler.Handle(command, cancellationToken));
        }
    }
}
