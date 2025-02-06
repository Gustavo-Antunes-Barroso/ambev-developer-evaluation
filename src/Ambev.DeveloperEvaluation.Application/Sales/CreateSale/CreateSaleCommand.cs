using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public DateTime CreatedAt { get; set; }
        public Guid CustomerId { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmount { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmountWithDiscount { get; set; }
        public bool Canceled { get; set; }
        public Guid SubsidiaryId { get; set; }
        public CreateSaleProductsCommand[] Products { get; set; } = new CreateSaleProductsCommand[0];
    }

    public class CreateSaleProductsCommand
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
