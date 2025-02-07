using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories.Sale;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleHandlersTest
{
    public class DeleteSaleHandlerTest
    {
        private readonly DeleteSaleHandler _handler;
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleProductRepository _saleProductRepository;

        public DeleteSaleHandlerTest()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _saleProductRepository = Substitute.For<ISaleProductRepository>();

            _saleRepository.MongoDbDeleteAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));
            _saleRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));
            _saleProductRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));

            _handler = new DeleteSaleHandler(_saleRepository, _saleProductRepository);
        }

        [Fact]
        public async Task DeleteSale_ReturnSuccess_Async()
        {
            DeleteSaleCommand command = new DeleteSaleCommand() { Id = Guid.NewGuid() };
            CancellationToken cancellationToken = new CancellationToken(false);
            
            var result = await _handler.Handle(command, cancellationToken);
            Assert.True(result.Sucess);
        }

        [Fact]
        public async Task DeleteSale_ReturnValidationException_Async()
        {
            DeleteSaleCommand command = new DeleteSaleCommand() { Id = Guid.Empty };
            CancellationToken cancellationToken = new CancellationToken(false);

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, cancellationToken));
        }
    }
}
