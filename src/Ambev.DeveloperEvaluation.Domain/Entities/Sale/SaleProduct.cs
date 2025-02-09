using Ambev.DeveloperEvaluation.Common.Util;
using Ambev.DeveloperEvaluation.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sale
{
    public class SaleProduct : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        public Guid SaleId { get; set; }
        [BsonRepresentation(BsonType.String)]
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

        public void SetDiscount(decimal discount) { Discount = discount; }

        public void CalculateAmount()
        {
            TotalAmount = MathOperations.RoundDecimalWithTwoPlaces(CalculateTotalAmount());
        }

        public void CalculateAmountWithDiscount()
        {
            if(TotalAmount <= 0)
                TotalAmount = CalculateTotalAmount();

            TotalAmountWithDiscount = MathOperations.RoundDecimalWithTwoPlaces(TotalAmount - (TotalAmount * (Discount / 100)));
        }

        private decimal CalculateTotalAmount()
            => Price * Quantity;
    }
}
