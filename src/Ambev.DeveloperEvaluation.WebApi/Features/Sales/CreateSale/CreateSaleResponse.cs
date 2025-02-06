﻿using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleResponse
    {
        public DateTime CreatedAt { get; set; }
        public Guid CustomerId { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmount { get; set; }
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public decimal TotalAmountWithDiscount { get; set; }
        [RegularExpression(@"^(0|-?\d{0,3}(\.\d{0,2})?)$")]
        public decimal Discount { get; set; } = 0;
        public bool Canceled { get; set; }
        public Guid SubsidiaryId { get; set; }
        public CreateSaleProductResponse[] Products { get; set; } = new CreateSaleProductResponse[0];
        public CreateSaleCustomerResponse Customer { get; set; } = new();
        public CreateSaleSubsidiaryResponse Subsidiary { get; set; } = new();


        public class CreateSaleProductResponse
        {
            public Guid Id { get; set; }
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
        }

        public class CreateSaleCustomerResponse
        {
            public Guid Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
        }

        public class CreateSaleSubsidiaryResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string State { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
        }
    }
}
