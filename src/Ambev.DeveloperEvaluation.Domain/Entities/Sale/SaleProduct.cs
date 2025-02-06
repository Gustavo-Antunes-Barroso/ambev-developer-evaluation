using Ambev.DeveloperEvaluation.Common.Util;
using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sale
{
    public class SaleProduct : BaseEntity
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal Price { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmount { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmountWithDiscount { get; set; }
        public int Quantity { get; set; }
        [RegularExpression(@"^(0|-?\d{0,3}(\.\d{0,2})?)$")]
        public decimal Discount { get; set; }

        public void CalculateSale(decimal discount)
        {
            Discount = discount;
            TotalAmount = MathOperations.RoundDecimalWithTwoPlaces(CalculateTotalAmount());
            TotalAmountWithDiscount = MathOperations.RoundDecimalWithTwoPlaces(CalculateTotalAmountWithDiscount(TotalAmount));
        }

        private decimal CalculateTotalAmountWithDiscount(decimal total)
            => total - (total * (Discount / 100));

        private decimal CalculateTotalAmount()
            => Price * Quantity;
    }
}
