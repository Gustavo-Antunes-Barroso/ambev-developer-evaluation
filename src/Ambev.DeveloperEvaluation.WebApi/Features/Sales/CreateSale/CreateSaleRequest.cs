namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithDiscount { get; set; }
        public bool Canceled { get; set; }
        public Guid SubsidiaryId { get; set; }
        public CreateSaleProductsRequest[] Products { get; set; } = new CreateSaleProductsRequest[0];
    }

    public class CreateSaleProductsRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithDiscount { get; set; }
        public decimal Discount { get; set; }
    }
}
