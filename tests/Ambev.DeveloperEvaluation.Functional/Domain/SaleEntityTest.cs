using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Domain
{
    public class SaleEntityTest
    {
        private Sale _entity;
        public SaleEntityTest()
        {
            _entity = new();
        }

        [Fact]
        public void Can_SetProducts()
        {
            _entity.SetProducts(new[] { new SaleProduct() });
            Assert.NotEmpty(_entity.Products);
        }

        [Fact]
        public void Can_SetCustomer()
        {
            _entity.SetCustomer(new SaleCustomer() { Id = Guid.NewGuid() });
            Assert.NotEqual(Guid.Empty, _entity.Customer.Id);
        }

        [Fact]
        public void Can_SetSubsidiary()
        {
            _entity.SetSubsidiary(new SaleSubsidiary() { Id = Guid.NewGuid()});
            Assert.NotEqual(Guid.Empty, _entity.Subsidiary.Id);
        }

        [Theory]
        [InlineData(9)]
        [InlineData(15)]
        public void Can_GetDiscount(int quantity)
        {
            _entity.SetProducts(new[] { new SaleProduct { Quantity = quantity } });
            Assert.True(_entity.GetDiscount() > 0);
        }

        [Fact]
        public void Can_SetProductsSaleId()
        {
            Guid saleId = Guid.NewGuid();
            _entity.SetProducts(new[] { new SaleProduct() });
            _entity.SetProductsSaleId(saleId);

            Assert.Equal(saleId, _entity.Products.First().SaleId);
        }

        [Fact]
        public void Can_CalculateSale()
        {
            _entity.Discount = 20;
            _entity.SetProducts(new[] { new SaleProduct() { Price = 15.50M, Quantity = 10 } });
            _entity.CalculateSale();

            decimal expectedTotalAmount = 155.00M;
            decimal expectedTotalAmountWithDiscount = 124.00M;

            Assert.Equal(_entity.TotalAmount, expectedTotalAmount);
            Assert.Equal(_entity.TotalAmountWithDiscount, expectedTotalAmountWithDiscount);
        }
    }
}
