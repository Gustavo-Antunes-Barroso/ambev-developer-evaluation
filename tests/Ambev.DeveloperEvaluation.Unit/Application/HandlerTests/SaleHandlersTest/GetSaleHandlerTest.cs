using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Base;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Entities;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.HandlerTests.SaleHandlersTest
{
    public class GetSaleHandlerTest : TestBase
    {
        private readonly GetSaleHandler _hander;
        private Sale sale;
        public GetSaleHandlerTest()
        {
            _hander = new GetSaleHandler(_saleRepository, _saleProductRepository, _mapper);
            sale = SaleEntityData.GenerateRandomSaleData();
        }

        [Fact]
        public async Task GetCompleteSaleById_ReturnSale_Success_Async()
        {

            _saleRepository.MongoDbGetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).ReturnsNull();
            _saleRepository.GetCompleteSaleByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(sale);

            var result = await _hander.Handle(new GetSaleCommand { Id = Guid.NewGuid() }, new CancellationToken(false));

            Assert.NotNull(result);
            Assert.IsType<GetSaleResult>(result);
        }

        [Fact]
        public async Task MongoDbGetAsync_ReturnSale_Success_Async()
        {
            _saleRepository.MongoDbGetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(sale);

            var result = await _hander.Handle(new GetSaleCommand { Id = Guid.NewGuid() }, new CancellationToken(false));

            Assert.NotNull(result);
            Assert.IsType<GetSaleResult>(result);
        }

        [Fact]
        public async Task GetCompleteSale_ReturnNotFound_Async()
        {
            _saleRepository.MongoDbGetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).ReturnsNull();
            _saleRepository.GetCompleteSaleByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNull();

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _hander.Handle(new GetSaleCommand { Id = Guid.NewGuid() }, new CancellationToken(false)));
        }

        [Fact]
        public async Task GetCompleteSale_ReturnValidationException_Async()
        {
            _saleRepository.MongoDbGetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).ReturnsNull();
            _saleRepository.GetCompleteSaleByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNull();

            await Assert.ThrowsAsync<ValidationException>(
                () => _hander.Handle(new GetSaleCommand { Id = Guid.Empty }, new CancellationToken(false)));
        }
    }
}
