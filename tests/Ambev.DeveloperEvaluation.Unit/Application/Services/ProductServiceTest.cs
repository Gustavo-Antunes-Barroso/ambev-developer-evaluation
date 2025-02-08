using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Application.Shared.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Unit.Application.Base;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.CommomData;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Entities;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Services
{
    public class ProductServiceTest : TestBase
    {
        private readonly ProductService _service;
        private UpsertSaleCommand command;

        public ProductServiceTest()
        {
            _service = new (_productRepository, _mapper);
            command = UpsertSaleCommandData.GenerateValidRandomUpsertSaleCommand();
        }

        [Fact]
        public async Task GetAndValidateProducts_ReturnSuccess_Async()
        {
            _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new Product() { Id = Guid.NewGuid() });
            SaleProduct[] result = await _service.GetAndValidateUpsertSaleProducts(command, cancellationToken);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAndValidateProducts_ReturnKeyNotFoundException_Async()
        {
            _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNull();

            await Assert.ThrowsAnyAsync<KeyNotFoundException>(() 
                => _service.GetAndValidateUpsertSaleProducts(command, cancellationToken));
        }

        [Fact]
        public void CalculateProductsValues_Success()
        {
            var products = SaleProductEntityData.GenerateRandomListSaleProductData();
            _service.CalculateProductsValues(products, 10, cancellationToken);

            Assert.True(!products.Any(x => x.Discount < 0));
            Assert.True(!products.Any(x => x.TotalAmount < 0));
            Assert.True(!products.Any(x => x.TotalAmountWithDiscount < 0));
        }

        [Fact]
        public void ValidateClientValues_Sucess()
        {
            UpsertSaleCommand command = UpsertSaleCommandData.GenerateValidRandomUpsertSaleCommand();
            Sale sale = _mapper.Map<Sale>(command);
            _service.ValidateClientValues(command, sale);
            
            Assert.Same(command, command);
            Assert.Same(sale, sale);
        }

        [Fact]
        public void ValidateClientValues_TotalAmountWrong_Returns_InvalidOperationException()
        {
            UpsertSaleCommand command = UpsertSaleCommandData.GenerateValidRandomUpsertSaleCommand();
            Sale sale = _mapper.Map<Sale>(command);
            sale.TotalAmount = 0;

            Assert.Throws<InvalidOperationException>(() => _service.ValidateClientValues(command, sale));
        }

        [Fact]
        public void ValidateClientValues_TotalAmountWithDiscountWrong_Returns_InvalidOperationException()
        {
            UpsertSaleCommand command = UpsertSaleCommandData.GenerateValidRandomUpsertSaleCommand();
            Sale sale = _mapper.Map<Sale>(command);
            sale.TotalAmountWithDiscount = 0;

            Assert.Throws<InvalidOperationException>(() => _service.ValidateClientValues(command, sale));
        }

        [Fact]
        public void ValidateClientValues_ValuesProductsWrong_Returns_InvalidOperationException()
        {
            UpsertSaleCommand command = UpsertSaleCommandData.GenerateValidRandomUpsertSaleCommand();
            Sale sale = _mapper.Map<Sale>(command);
            sale.Products.First().Discount = 30;

            Assert.Throws<InvalidOperationException>(() => _service.ValidateClientValues(command, sale));
        }
    }
}
