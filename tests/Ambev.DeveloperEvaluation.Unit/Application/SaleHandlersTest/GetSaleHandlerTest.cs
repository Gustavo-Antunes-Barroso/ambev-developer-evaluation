using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.SaleHandlersTest.CommomData;
using AutoMapper;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.SaleHandlersTest
{
    public class GetSaleHandlerTest
    {
        private readonly GetSaleHandler _hander;
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleProductRepository _saleProductRepository;
        private readonly IMapper _mapper;

        public GetSaleHandlerTest()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _saleProductRepository = Substitute.For<ISaleProductRepository>();
            _mapper = Substitute.For<IMapper>();

            _hander = new GetSaleHandler(_saleRepository, _saleProductRepository, _mapper);
        }

        [Fact]
        public async Task GetCompleteSaleById_ReturnSale_Success_Async()
        {
            Sale sale = SaleEntityData.GenerateRandomSaleData();
            _saleRepository.MongoDbGetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).ReturnsNull();
            _saleRepository.GetCompleteSaleByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(sale);

            _mapper.Map<GetSaleResult>(sale).Returns(new GetSaleResult());

            var result = await _hander.Handle(new GetSaleCommand { Id = Guid.NewGuid() }, new CancellationToken(false));

            Assert.NotNull(result);
            Assert.IsType<GetSaleResult>(result);
        }

        [Fact]
        public async Task MongoDbGetAsync_ReturnSale_Success_Async()
        {
            Sale sale = SaleEntityData.GenerateRandomSaleData();
            _saleRepository.MongoDbGetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(sale);
            _mapper.Map<GetSaleResult>(sale).Returns(new GetSaleResult());

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
