using Ambev.DeveloperEvaluation.Application.Shared.Results;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand : IRequest<UpsertSaleResult>
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CustomerId { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmount { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmountWithDiscount { get; set; }
        public bool Canceled { get; set; }
        public Guid SubsidiaryId { get; set; }
        public UpdateSaleProductCommand[] Products { get; set; } = new UpdateSaleProductCommand[0];
    }

    public class UpdateSaleProductCommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal Price { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmount { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmountWithDiscount { get; set; }
        [RegularExpression(@"^(0|-?\d{0,3}(\.\d{0,2})?)$")]
        public decimal Discount { get; set; }
    }
}
