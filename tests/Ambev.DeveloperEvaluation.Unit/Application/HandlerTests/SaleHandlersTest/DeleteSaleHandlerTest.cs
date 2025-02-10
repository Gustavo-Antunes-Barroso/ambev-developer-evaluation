using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Unit.Application.Base;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.HandlerTests.SaleHandlersTest
{
    public class DeleteSaleHandlerTest : TestBase
    {
        private readonly DeleteSaleHandler _handler;

        public DeleteSaleHandlerTest()
        {
            _saleRepository.MongoDbDeleteAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));
            _saleRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));
            _saleProductRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));

            _handler = new DeleteSaleHandler(_saleRepository, _saleProductRepository, _rabbitMQProducer);
        }

        [Fact]
        public async Task DeleteSale_ReturnSuccess_Async()
        {
            DeleteSaleCommand command = new DeleteSaleCommand() { Id = Guid.NewGuid() };

            var result = await _handler.Handle(command, cancellationToken);
            Assert.True(result.Sucess);
        }

        [Fact]
        public async Task DeleteSale_ReturnValidationException_Async()
        {
            DeleteSaleCommand command = new DeleteSaleCommand() { Id = Guid.Empty };

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, cancellationToken));
        }
    }
}
