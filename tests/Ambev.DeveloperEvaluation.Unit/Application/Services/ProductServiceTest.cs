using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Unit.Application.Base;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.CommomData;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Services
{
    public class ProductServiceTest : TestBase
    {
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            _service = new ProductService(_productRepository, _mapper);
        }

        [Fact]
        public async Task GetAndValidateProducts_ReturnSuccess_Async()
        {
            _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new Product() { Id = Guid.NewGuid() });
            var command = UpsertSaleCommandData.GenerateValidRandomUpsertSaleCommand();
            SaleProduct[] result = await _service.GetAndValidateUpsertSaleProducts(command, new CancellationToken(false));

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAndValidateProducts_ReturnKeyNotFoundException_Async()
        {
            _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNull();
            var command = UpsertSaleCommandData.GenerateValidRandomUpsertSaleCommand();

            await Assert.ThrowsAnyAsync<KeyNotFoundException>(() 
                => _service.GetAndValidateUpsertSaleProducts(command, new CancellationToken(false)));
        }
    }
}
