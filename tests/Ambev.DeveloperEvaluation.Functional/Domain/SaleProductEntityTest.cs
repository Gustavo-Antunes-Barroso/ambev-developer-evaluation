using Ambev.DeveloperEvaluation.Common.Util;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Functional.Domain
{
    public class SaleProductEntityTest
    {
        public SaleProduct _entity;
        public SaleProductEntityTest()
        {
            _entity = new();
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public void Can_SetDiscount(int discount)
        {
            _entity.SetDiscount(discount);
            Assert.True(_entity.Discount > 0);
        }

        [Fact]
        public void Can_CalculateAmount()
        {
            _entity.Price = 9.25M;
            _entity.Quantity = 10;
            decimal expectedAmount = MathOperations.RoundDecimalWithTwoPlaces(_entity.Price * _entity.Quantity);

            _entity.CalculateAmount();

            Assert.Equal(expectedAmount, _entity.TotalAmount);
        }

        [Fact]
        public void Can_CalculateAmountWithDiscount()
        {
            _entity.Price = 9.25M;
            _entity.Quantity = 10;
            _entity.SetDiscount(20);
            decimal totalAmount = _entity.Price * _entity.Quantity;
            decimal expectedAmountWithDiscount = MathOperations.RoundDecimalWithTwoPlaces(totalAmount - (totalAmount * (_entity.Discount / 100)));
            
            _entity.CalculateAmountWithDiscount();

            Assert.Equal(expectedAmountWithDiscount, _entity.TotalAmountWithDiscount);
        }
    }
}
