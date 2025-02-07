using Ambev.DeveloperEvaluation.Common.Util;
using Ambev.DeveloperEvaluation.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sale
{
    public class Sale : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid CustomerId { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmount { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmountWithDiscount { get; set; }
        [RegularExpression(@"^(0|-?\d{0,3}(\.\d{0,2})?)$")]
        public decimal Discount {  get; set; } = 0;
        public bool Canceled { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Guid SubsidiaryId { get; set; }
        public SaleProduct[] Products { get; private set; } = new SaleProduct[0];
        public SaleCustomer Customer { get; private set; } = new();
        public SaleSubsidiary Subsidiary { get; private set; } = new();

        public Sale SetProducts(SaleProduct[] products)
        {
            Products = products;
            return this;
        }

        public Sale SetCustomer(SaleCustomer customer)
        {
            Customer = customer;
            return this;
        }

        public Sale SetSubsidiary(SaleSubsidiary subsidiary)
        {
            Subsidiary = subsidiary;
            return this;
        }

        public decimal GetDiscount()
        {
            if (Products.Any(x => x.Quantity > 9 && x.Quantity <= 20))
                Discount = 20;
            else if (Products.Any(x => x.Quantity > 4 && x.Quantity <= 9))
                Discount = 10;

            return Discount;
        }

        public void SetProductsSaleId(Guid saleId)
        {
            Products = Products.Select(x => { x.SaleId = saleId; return x; }).ToArray();
        }

        public void CalculateSale()
        {
            TotalAmount = MathOperations.RoundDecimalWithTwoPlaces(CalculateTotalSaleWithoutDiscount());
            TotalAmountWithDiscount = MathOperations.RoundDecimalWithTwoPlaces(CalculateTotalSaleWithDiscount(TotalAmount));
        }

        private decimal CalculateTotalSaleWithDiscount(decimal total)
            => total - (total * (Discount / 100));

        private decimal CalculateTotalSaleWithoutDiscount()
            => Products.Sum(x => (x.Price * x.Quantity));
    }
}
