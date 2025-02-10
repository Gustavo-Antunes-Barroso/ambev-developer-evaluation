namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithDiscount { get; set; }
        public bool Canceled { get; set; }
        public Guid SubsidiaryId { get; set; }
        public UpdateSaleProductsRequest[] Products { get; set; } = new UpdateSaleProductsRequest[0];
    }

    public class UpdateSaleProductsRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithDiscount { get; set; }
        public decimal Discount { get; set; }
    }
}
